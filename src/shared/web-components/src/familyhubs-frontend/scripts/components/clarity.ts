import {ConsentCookie, isValidConsentCookie} from "./cookie-functions";

declare global {
    interface Window {
        clarity: any;
    }
}

let clarityEnabled: boolean = false;

export default function initClarity(clarityId: string, userConsent: ConsentCookie) {
    // if the environment doesn't have a measurement id, don't load analytics
    clarityEnabled = Boolean(clarityId);
    if (!clarityEnabled) return;

    window.clarity = window.clarity || function() {
        (window.clarity.q = window.clarity.q || []).push(arguments)
    }
    
    let previousConsent = Boolean(window.sessionStorage.getItem('_cltk'));
    if (userConsent && isValidConsentCookie(userConsent) && userConsent.analytics != previousConsent) {
        updateConsent(userConsent.analytics);
    }

    loadClarityScript(clarityId);
}

export function updateConsent(consent: boolean) {
    if (!clarityEnabled) return;
    window.clarity('consent', consent);
}

function loadClarityScript(clarityId) {
    const f = document.getElementsByTagName('script')[0];
    const j = document.createElement('script');
    j.async = true;
    j.src = 'https://www.clarity.ms/tag/' + clarityId;
    f.parentNode.insertBefore(j, f);
}
