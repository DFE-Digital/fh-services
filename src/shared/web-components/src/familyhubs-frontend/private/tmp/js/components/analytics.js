//todo: consent mode debugging/check: https://developers.google.com/tag-platform/devguides/consent-debugging
import { getConsentCookie, isValidConsentCookie } from './cookie-functions';
import { toOutcode } from './postcode';
function gtag(command, ...args) {
    window.dataLayer = window.dataLayer || [];
    window.dataLayer.push(arguments);
}
let GaMeasurementId = '';
//todo: use prototype? (or class?)
// (having an object (prototype/class) will ensure that GaMeasurementId will have already been set)
export default function initAnalytics(gaMeasurementId) {
    // if the environment doesn't have a measurement id, don't load analytics
    if (!Boolean(gaMeasurementId)) {
        return;
    }
    GaMeasurementId = gaMeasurementId;
    setDefaultConsent();
    loadGaScript(gaMeasurementId);
    gtag('js', new Date());
    const pageViewParams = getPiiSafePageView(gaMeasurementId);
    // set the config for auto generated events other than page_view
    gtag('config', gaMeasurementId, {
        send_page_view: false, //disable auto page_view measurement
        page_path: pageViewParams.page_path,
        page_location: pageViewParams.page_location,
        page_referrer: pageViewParams.referrer,
        cookie_flags: 'secure'
    });
    const userConsent = getConsentCookie();
    if (userConsent && isValidConsentCookie(userConsent) && userConsent.analytics) {
        updateAnalyticsStorageConsent(true);
    }
    sendPageViewEvent();
    sendFilterPageCustomEvent();
}
function setDefaultConsent() {
    gtag('consent', 'default', {
        'analytics_storage': 'denied'
    });
    gtag('set', 'url_passthrough', true);
}
export function updateAnalyticsStorageConsent(granted, delayMs) {
    let options = {
        'analytics_storage': granted ? 'granted' : 'denied'
    };
    if (typeof delayMs !== 'undefined') {
        options['wait_for_update'] = delayMs;
    }
    gtag('consent', 'update', options);
}
export function sendPageViewEvent() {
    // send the page_view event manually (https://developers.google.com/analytics/devguides/collection/gtagjs/pages#default_behavior)
    gtag('event', 'page_view', getPiiSafePageView(GaMeasurementId));
}
export function sendFilterPageCustomEvent() {
    //todo: make filter page only
    const element = document.getElementById('results');
    const totalResults = element === null || element === void 0 ? void 0 : element.getAttribute('data-total-results');
    if (totalResults === undefined)
        return;
    gtag('event', 'filter_page', {
        'total_results': totalResults
    });
}
export function sendAnalyticsCustomEvent(accepted, source) {
    gtag('event', 'analytics', {
        'accepted': accepted,
        'source': source
    });
}
function loadGaScript(gaMeasurementId) {
    const f = document.getElementsByTagName('script')[0];
    const j = document.createElement('script');
    j.async = true;
    j.src = 'https://www.googletagmanager.com/gtag/js?id=' + gaMeasurementId;
    f.parentNode.insertBefore(j, f);
}
function getPiiSafePageView(gaMeasurementId) {
    const pageView = {
        page_title: document.title,
        send_to: gaMeasurementId,
        referrer: '',
        page_location: '',
        page_path: ''
    };
    //todo: set as referrer or page_referrer in pageView - does it matter? is it only picking it up from the config?
    //todo: get piisafe referrer function
    if (document.referrer !== '') {
        const referrerUrl = new URL(document.referrer);
        const piiSafeReferrerQueryString = getPiiSafeQueryString(referrerUrl.search);
        if (piiSafeReferrerQueryString == null) {
            pageView.referrer = document.referrer;
        }
        else {
            const urlArray = document.referrer.split('?');
            pageView.referrer = urlArray[0] + piiSafeReferrerQueryString;
        }
    }
    const piiSafeQueryString = getPiiSafeQueryString(window.location.search);
    if (piiSafeQueryString == null) {
        pageView.page_location = window.location.href;
        pageView.page_path = window.location.pathname + window.location.search;
        return pageView;
    }
    const urlArray = window.location.href.split('?');
    pageView.page_location = urlArray[0] + piiSafeQueryString;
    pageView.page_path = window.location.pathname + piiSafeQueryString;
    return pageView;
}
function getPiiSafeQueryString(queryString) {
    //todo: for safety, convert to lowercase, so that if the user changes the case of the url, we still don't collect pii
    const queryParams = new URLSearchParams(queryString);
    let postcode = queryParams.get('postcode');
    if (postcode == null) {
        // null indicates original query params were already pii safe
        return null;
    }
    postcode = toOutcode(postcode);
    queryParams.set('postcode', postcode);
    queryParams.delete('latitude');
    queryParams.delete('longitude');
    return '?' + queryParams.toString();
}

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImNvbXBvbmVudHMvYW5hbHl0aWNzLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUNBLDRHQUE0RztBQUU1RyxPQUFPLEVBQUUsZ0JBQWdCLEVBQUUsb0JBQW9CLEVBQUUsTUFBTSxvQkFBb0IsQ0FBQTtBQUMzRSxPQUFPLEVBQUUsU0FBUyxFQUFFLE1BQU0sWUFBWSxDQUFBO0FBRXRDLFNBQVMsSUFBSSxDQUFDLE9BQWUsRUFBRSxHQUFHLElBQVc7SUFDekMsTUFBTSxDQUFDLFNBQVMsR0FBRyxNQUFNLENBQUMsU0FBUyxJQUFJLEVBQUUsQ0FBQztJQUMxQyxNQUFNLENBQUMsU0FBUyxDQUFDLElBQUksQ0FBQyxTQUFTLENBQUMsQ0FBQztBQUNyQyxDQUFDO0FBRUQsSUFBSSxlQUFlLEdBQVcsRUFBRSxDQUFDO0FBRWpDLGtDQUFrQztBQUNsQyxtR0FBbUc7QUFDbkcsTUFBTSxDQUFDLE9BQU8sVUFBVSxhQUFhLENBQUMsZUFBdUI7SUFFekQseUVBQXlFO0lBQ3pFLElBQUksQ0FBQyxPQUFPLENBQUMsZUFBZSxDQUFDLEVBQUUsQ0FBQztRQUM1QixPQUFPO0lBQ1gsQ0FBQztJQUVELGVBQWUsR0FBRyxlQUFlLENBQUM7SUFFbEMsaUJBQWlCLEVBQUUsQ0FBQztJQUVwQixZQUFZLENBQUMsZUFBZSxDQUFDLENBQUM7SUFFOUIsSUFBSSxDQUFDLElBQUksRUFBRSxJQUFJLElBQUksRUFBRSxDQUFDLENBQUM7SUFFdkIsTUFBTSxjQUFjLEdBQUcsa0JBQWtCLENBQUMsZUFBZSxDQUFDLENBQUM7SUFFM0QsZ0VBQWdFO0lBQ2hFLElBQUksQ0FBQyxRQUFRLEVBQUUsZUFBZSxFQUFFO1FBQzVCLGNBQWMsRUFBRSxLQUFLLEVBQUUsb0NBQW9DO1FBQzNELFNBQVMsRUFBRSxjQUFjLENBQUMsU0FBUztRQUNuQyxhQUFhLEVBQUUsY0FBYyxDQUFDLGFBQWE7UUFDM0MsYUFBYSxFQUFFLGNBQWMsQ0FBQyxRQUFRO1FBQ3RDLFlBQVksRUFBRSxRQUFRO0tBQ3pC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LHFIQUFxSDtJQUNySCxNQUFNLFdBQVcsR0FBRyxJQUFJLGVBQWUsQ0FBQyxXQUFXLENBQUMsQ0FBQztJQUVyRCxJQUFJLFFBQVEsR0FBRyxXQUFXLENBQUMsR0FBRyxDQUFDLFVBQVUsQ0FBQyxDQUFDO0lBQzNDLElBQUksUUFBUSxJQUFJLElBQUksRUFBRSxDQUFDO1FBQ25CLDZEQUE2RDtRQUM3RCxPQUFPLElBQUksQ0FBQztJQUNoQixDQUFDO0lBRUQsUUFBUSxHQUFHLFNBQVMsQ0FBQyxRQUFRLENBQUMsQ0FBQztJQUMvQixXQUFXLENBQUMsR0FBRyxDQUFDLFVBQVUsRUFBRSxRQUFRLENBQUMsQ0FBQztJQUN0QyxXQUFXLENBQUMsTUFBTSxDQUFDLFVBQVUsQ0FBQyxDQUFDO0lBQy9CLFdBQVcsQ0FBQyxNQUFNLENBQUMsV0FBVyxDQUFDLENBQUM7SUFFaEMsT0FBTyxHQUFHLEdBQUcsV0FBVyxDQUFDLFFBQVEsRUFBRSxDQUFDO0FBQ3hDLENBQUMiLCJmaWxlIjoiY29tcG9uZW50cy9hbmFseXRpY3MuanMiLCJzb3VyY2VzQ29udGVudCI6WyJcclxuLy90b2RvOiBjb25zZW50IG1vZGUgZGVidWdnaW5nL2NoZWNrOiBodHRwczovL2RldmVsb3BlcnMuZ29vZ2xlLmNvbS90YWctcGxhdGZvcm0vZGV2Z3VpZGVzL2NvbnNlbnQtZGVidWdnaW5nXHJcblxyXG5pbXBvcnQgeyBnZXRDb25zZW50Q29va2llLCBpc1ZhbGlkQ29uc2VudENvb2tpZSB9IGZyb20gJy4vY29va2llLWZ1bmN0aW9ucydcclxuaW1wb3J0IHsgdG9PdXRjb2RlIH0gZnJvbSAnLi9wb3N0Y29kZSdcclxuXHJcbmZ1bmN0aW9uIGd0YWcoY29tbWFuZDogc3RyaW5nLCAuLi5hcmdzOiBhbnlbXSk6IHZvaWQge1xyXG4gICAgd2luZG93LmRhdGFMYXllciA9IHdpbmRvdy5kYXRhTGF5ZXIgfHwgW107XHJcbiAgICB3aW5kb3cuZGF0YUxheWVyLnB1c2goYXJndW1lbnRzKTtcclxufVxyXG5cclxubGV0IEdhTWVhc3VyZW1lbnRJZDogc3RyaW5nID0gJyc7XHJcblxyXG4vL3RvZG86IHVzZSBwcm90b3R5cGU/IChvciBjbGFzcz8pXHJcbi8vIChoYXZpbmcgYW4gb2JqZWN0IChwcm90b3R5cGUvY2xhc3MpIHdpbGwgZW5zdXJlIHRoYXQgR2FNZWFzdXJlbWVudElkIHdpbGwgaGF2ZSBhbHJlYWR5IGJlZW4gc2V0KVxyXG5leHBvcnQgZGVmYXVsdCBmdW5jdGlvbiBpbml0QW5hbHl0aWNzKGdhTWVhc3VyZW1lbnRJZDogc3RyaW5nKSB7XHJcblxyXG4gICAgLy8gaWYgdGhlIGVudmlyb25tZW50IGRvZXNuJ3QgaGF2ZSBhIG1lYXN1cmVtZW50IGlkLCBkb24ndCBsb2FkIGFuYWx5dGljc1xyXG4gICAgaWYgKCFCb29sZWFuKGdhTWVhc3VyZW1lbnRJZCkpIHtcclxuICAgICAgICByZXR1cm47XHJcbiAgICB9XHJcblxyXG4gICAgR2FNZWFzdXJlbWVudElkID0gZ2FNZWFzdXJlbWVudElkO1xyXG5cclxuICAgIHNldERlZmF1bHRDb25zZW50KCk7XHJcblxyXG4gICAgbG9hZEdhU2NyaXB0KGdhTWVhc3VyZW1lbnRJZCk7XHJcblxyXG4gICAgZ3RhZygnanMnLCBuZXcgRGF0ZSgpKTtcclxuXHJcbiAgICBjb25zdCBwYWdlVmlld1BhcmFtcyA9IGdldFBpaVNhZmVQYWdlVmlldyhnYU1lYXN1cmVtZW50SWQpO1xyXG5cclxuICAgIC8vIHNldCB0aGUgY29uZmlnIGZvciBhdXRvIGdlbmVyYXRlZCBldmVudHMgb3RoZXIgdGhhbiBwYWdlX3ZpZXdcclxuICAgIGd0YWcoJ2NvbmZpZycsIGdhTWVhc3VyZW1lbnRJZCwge1xyXG4gICAgICAgIHNlbmRfcGFnZV92aWV3OiBmYWxzZSwgLy9kaXNhYmxlIGF1dG8gcGFnZV92aWV3IG1lYXN1cmVtZW50XHJcbiAgICAgICAgcGFnZV9wYXRoOiBwYWdlVmlld1BhcmFtcy5wYWdlX3BhdGgsXHJcbiAgICAgICAgcGFnZV9sb2NhdGlvbjogcGFnZVZpZXdQYXJhbXMucGFnZV9sb2NhdGlvbixcclxuICAgICAgICBwYWdlX3JlZmVycmVyOiBwYWdlVmlld1BhcmFtcy5yZWZlcnJlcixcclxuICAgICAgICBjb29raWVfZmxhZ3M6ICdzZWN1cmUnXHJcbiAgICB9KTtcclxuXHJcbiAgICBjb25zdCB1c2VyQ29uc2VudCA9IGdldENvbnNlbnRDb29raWUoKTtcclxuICAgIGlmICh1c2VyQ29uc2VudCAmJiBpc1ZhbGlkQ29uc2VudENvb2tpZSh1c2VyQ29uc2VudCkgJiYgdXNlckNvbnNlbnQuYW5hbHl0aWNzKSB7XHJcbiAgICAgICAgdXBkYXRlQW5hbHl0aWNzU3RvcmFnZUNvbnNlbnQodHJ1ZSk7XHJcbiAgICB9XHJcblxyXG4gICAgc2VuZFBhZ2VWaWV3RXZlbnQoKTtcclxuICAgIHNlbmRGaWx0ZXJQYWdlQ3VzdG9tRXZlbnQoKTtcclxufVxyXG5cclxuZnVuY3Rpb24gc2V0RGVmYXVsdENvbnNlbnQoKSB7XHJcbiAgICBndGFnKCdjb25zZW50JywgJ2RlZmF1bHQnLCB7XHJcbiAgICAgICAgJ2FuYWx5dGljc19zdG9yYWdlJzogJ2RlbmllZCdcclxuICAgIH0pO1xyXG5cclxuICAgIGd0YWcoJ3NldCcsICd1cmxfcGFzc3Rocm91Z2gnLCB0cnVlKTtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIHVwZGF0ZUFuYWx5dGljc1N0b3JhZ2VDb25zZW50KGdyYW50ZWQ6IGJvb2xlYW4sIGRlbGF5TXM/OiBudW1iZXIpIHtcclxuXHJcbiAgICBsZXQgb3B0aW9ucyA9IHtcclxuICAgICAgICAnYW5hbHl0aWNzX3N0b3JhZ2UnOiBncmFudGVkID8gJ2dyYW50ZWQnIDogJ2RlbmllZCdcclxuICAgIH07XHJcblxyXG4gICAgaWYgKHR5cGVvZiBkZWxheU1zICE9PSAndW5kZWZpbmVkJykge1xyXG4gICAgICAgIG9wdGlvbnNbJ3dhaXRfZm9yX3VwZGF0ZSddID0gZGVsYXlNcztcclxuICAgIH1cclxuXHJcbiAgICBndGFnKCdjb25zZW50JywgJ3VwZGF0ZScsIG9wdGlvbnMpO1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gc2VuZFBhZ2VWaWV3RXZlbnQoKSB7XHJcbiAgICAvLyBzZW5kIHRoZSBwYWdlX3ZpZXcgZXZlbnQgbWFudWFsbHkgKGh0dHBzOi8vZGV2ZWxvcGVycy5nb29nbGUuY29tL2FuYWx5dGljcy9kZXZndWlkZXMvY29sbGVjdGlvbi9ndGFnanMvcGFnZXMjZGVmYXVsdF9iZWhhdmlvcilcclxuICAgIGd0YWcoJ2V2ZW50JywgJ3BhZ2VfdmlldycsIGdldFBpaVNhZmVQYWdlVmlldyhHYU1lYXN1cmVtZW50SWQpKTtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIHNlbmRGaWx0ZXJQYWdlQ3VzdG9tRXZlbnQoKSB7XHJcbiAgICAvL3RvZG86IG1ha2UgZmlsdGVyIHBhZ2Ugb25seVxyXG4gICAgY29uc3QgZWxlbWVudCA9IGRvY3VtZW50LmdldEVsZW1lbnRCeUlkKCdyZXN1bHRzJyk7XHJcbiAgICBjb25zdCB0b3RhbFJlc3VsdHMgPSBlbGVtZW50Py5nZXRBdHRyaWJ1dGUoJ2RhdGEtdG90YWwtcmVzdWx0cycpO1xyXG4gICAgaWYgKHRvdGFsUmVzdWx0cyA9PT0gdW5kZWZpbmVkKVxyXG4gICAgICAgIHJldHVybjtcclxuXHJcbiAgICBndGFnKCdldmVudCcsICdmaWx0ZXJfcGFnZScsIHtcclxuICAgICAgICAndG90YWxfcmVzdWx0cyc6IHRvdGFsUmVzdWx0c1xyXG4gICAgfSk7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBzZW5kQW5hbHl0aWNzQ3VzdG9tRXZlbnQoYWNjZXB0ZWQ6IGJvb2xlYW4sIHNvdXJjZTogc3RyaW5nKSB7XHJcblxyXG4gICAgZ3RhZygnZXZlbnQnLCAnYW5hbHl0aWNzJywge1xyXG4gICAgICAgICdhY2NlcHRlZCc6IGFjY2VwdGVkLFxyXG4gICAgICAgICdzb3VyY2UnOiBzb3VyY2VcclxuICAgIH0pO1xyXG59XHJcblxyXG5mdW5jdGlvbiBsb2FkR2FTY3JpcHQoZ2FNZWFzdXJlbWVudElkOiBzdHJpbmcpIHtcclxuICAgIGNvbnN0IGYgPSBkb2N1bWVudC5nZXRFbGVtZW50c0J5VGFnTmFtZSgnc2NyaXB0JylbMF07XHJcbiAgICBjb25zdCBqID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnc2NyaXB0Jyk7XHJcbiAgICBqLmFzeW5jID0gdHJ1ZTtcclxuICAgIGouc3JjID0gJ2h0dHBzOi8vd3d3Lmdvb2dsZXRhZ21hbmFnZXIuY29tL2d0YWcvanM/aWQ9JyArIGdhTWVhc3VyZW1lbnRJZDtcclxuICAgIGYucGFyZW50Tm9kZS5pbnNlcnRCZWZvcmUoaiwgZik7XHJcbn1cclxuXHJcbmZ1bmN0aW9uIGdldFBpaVNhZmVQYWdlVmlldyhnYU1lYXN1cmVtZW50SWQ6IHN0cmluZykge1xyXG5cclxuICAgIGNvbnN0IHBhZ2VWaWV3ID0ge1xyXG4gICAgICAgIHBhZ2VfdGl0bGU6IGRvY3VtZW50LnRpdGxlLFxyXG4gICAgICAgIHNlbmRfdG86IGdhTWVhc3VyZW1lbnRJZCxcclxuICAgICAgICByZWZlcnJlcjogJycsXHJcbiAgICAgICAgcGFnZV9sb2NhdGlvbjogJycsXHJcbiAgICAgICAgcGFnZV9wYXRoOiAnJ1xyXG4gICAgfTtcclxuXHJcbiAgICAvL3RvZG86IHNldCBhcyByZWZlcnJlciBvciBwYWdlX3JlZmVycmVyIGluIHBhZ2VWaWV3IC0gZG9lcyBpdCBtYXR0ZXI/IGlzIGl0IG9ubHkgcGlja2luZyBpdCB1cCBmcm9tIHRoZSBjb25maWc/XHJcbiAgICAvL3RvZG86IGdldCBwaWlzYWZlIHJlZmVycmVyIGZ1bmN0aW9uXHJcbiAgICBpZiAoZG9jdW1lbnQucmVmZXJyZXIgIT09ICcnKSB7XHJcbiAgICAgICAgY29uc3QgcmVmZXJyZXJVcmwgPSBuZXcgVVJMKGRvY3VtZW50LnJlZmVycmVyKTtcclxuICAgICAgICBjb25zdCBwaWlTYWZlUmVmZXJyZXJRdWVyeVN0cmluZyA9IGdldFBpaVNhZmVRdWVyeVN0cmluZyhyZWZlcnJlclVybC5zZWFyY2gpO1xyXG4gICAgICAgIGlmIChwaWlTYWZlUmVmZXJyZXJRdWVyeVN0cmluZyA9PSBudWxsKSB7XHJcbiAgICAgICAgICAgIHBhZ2VWaWV3LnJlZmVycmVyID0gZG9jdW1lbnQucmVmZXJyZXI7XHJcbiAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgY29uc3QgdXJsQXJyYXkgPSBkb2N1bWVudC5yZWZlcnJlci5zcGxpdCgnPycpO1xyXG5cclxuICAgICAgICAgICAgcGFnZVZpZXcucmVmZXJyZXIgPSB1cmxBcnJheVswXSArIHBpaVNhZmVSZWZlcnJlclF1ZXJ5U3RyaW5nO1xyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcbiAgICBjb25zdCBwaWlTYWZlUXVlcnlTdHJpbmcgPSBnZXRQaWlTYWZlUXVlcnlTdHJpbmcod2luZG93LmxvY2F0aW9uLnNlYXJjaCk7XHJcblxyXG4gICAgaWYgKHBpaVNhZmVRdWVyeVN0cmluZyA9PSBudWxsKSB7XHJcbiAgICAgICAgcGFnZVZpZXcucGFnZV9sb2NhdGlvbiA9IHdpbmRvdy5sb2NhdGlvbi5ocmVmO1xyXG4gICAgICAgIHBhZ2VWaWV3LnBhZ2VfcGF0aCA9IHdpbmRvdy5sb2NhdGlvbi5wYXRobmFtZSArIHdpbmRvdy5sb2NhdGlvbi5zZWFyY2g7XHJcblxyXG4gICAgICAgIHJldHVybiBwYWdlVmlldztcclxuICAgIH1cclxuXHJcbiAgICBjb25zdCB1cmxBcnJheSA9IHdpbmRvdy5sb2NhdGlvbi5ocmVmLnNwbGl0KCc/Jyk7XHJcblxyXG4gICAgcGFnZVZpZXcucGFnZV9sb2NhdGlvbiA9IHVybEFycmF5WzBdICsgcGlpU2FmZVF1ZXJ5U3RyaW5nO1xyXG4gICAgcGFnZVZpZXcucGFnZV9wYXRoID0gd2luZG93LmxvY2F0aW9uLnBhdGhuYW1lICsgcGlpU2FmZVF1ZXJ5U3RyaW5nO1xyXG5cclxuICAgIHJldHVybiBwYWdlVmlldztcclxufVxyXG5cclxuZnVuY3Rpb24gZ2V0UGlpU2FmZVF1ZXJ5U3RyaW5nKHF1ZXJ5U3RyaW5nOiBzdHJpbmcpOiBzdHJpbmcgfCBudWxsIHtcclxuXHJcbiAgICAvL3RvZG86IGZvciBzYWZldHksIGNvbnZlcnQgdG8gbG93ZXJjYXNlLCBzbyB0aGF0IGlmIHRoZSB1c2VyIGNoYW5nZXMgdGhlIGNhc2Ugb2YgdGhlIHVybCwgd2Ugc3RpbGwgZG9uJ3QgY29sbGVjdCBwaWlcclxuICAgIGNvbnN0IHF1ZXJ5UGFyYW1zID0gbmV3IFVSTFNlYXJjaFBhcmFtcyhxdWVyeVN0cmluZyk7XHJcblxyXG4gICAgbGV0IHBvc3Rjb2RlID0gcXVlcnlQYXJhbXMuZ2V0KCdwb3N0Y29kZScpO1xyXG4gICAgaWYgKHBvc3Rjb2RlID09IG51bGwpIHtcclxuICAgICAgICAvLyBudWxsIGluZGljYXRlcyBvcmlnaW5hbCBxdWVyeSBwYXJhbXMgd2VyZSBhbHJlYWR5IHBpaSBzYWZlXHJcbiAgICAgICAgcmV0dXJuIG51bGw7XHJcbiAgICB9XHJcblxyXG4gICAgcG9zdGNvZGUgPSB0b091dGNvZGUocG9zdGNvZGUpO1xyXG4gICAgcXVlcnlQYXJhbXMuc2V0KCdwb3N0Y29kZScsIHBvc3Rjb2RlKTtcclxuICAgIHF1ZXJ5UGFyYW1zLmRlbGV0ZSgnbGF0aXR1ZGUnKTtcclxuICAgIHF1ZXJ5UGFyYW1zLmRlbGV0ZSgnbG9uZ2l0dWRlJyk7XHJcblxyXG4gICAgcmV0dXJuICc/JyArIHF1ZXJ5UGFyYW1zLnRvU3RyaW5nKCk7XHJcbn1cclxuIl19