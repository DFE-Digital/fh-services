import { getConsentCookie, setConsentCookie } from './cookie-functions';
import { nodeListForEach } from './helpers';
import { sendPageViewEvent, sendAnalyticsCustomEvent, updateAnalyticsStorageConsent } from './analytics';
import { updateConsent as updateClarityConsent } from "./clarity";
function CookiesPage($module) {
    this.$module = $module;
}
CookiesPage.prototype.init = function () {
    this.$cookiePage = this.$module;
    if (!this.$cookiePage) {
        return;
    }
    this.$cookieForm = this.$cookiePage.querySelector('.js-cookies-page-form');
    this.$cookieFormFieldsets = this.$cookieForm.querySelectorAll('.js-cookies-page-form-fieldset');
    this.$successNotification = this.$cookiePage.querySelector('.js-cookies-page-success');
    this.$successLink = this.$cookiePage.querySelector('.js-cookies-page-success-link');
    nodeListForEach(this.$cookieFormFieldsets, function ($cookieFormFieldset) {
        this.showUserPreference($cookieFormFieldset, getConsentCookie());
        /*        $cookieFormFieldset.removeAttribute('hidden')*/
    }.bind(this));
    // Show submit button
    //this.$cookieForm.querySelector('.js-cookies-form-button').removeAttribute('hidden')
    this.$cookieForm.addEventListener('submit', this.savePreferences.bind(this));
};
CookiesPage.prototype.savePreferences = function (event) {
    // Stop default form submission behaviour
    event.preventDefault();
    var preferences = {};
    nodeListForEach(this.$cookieFormFieldsets, function ($cookieFormFieldset) {
        var cookieType = this.getCookieType($cookieFormFieldset);
        var selectedItem = $cookieFormFieldset.querySelector('input[name=' + cookieType + ']:checked').value;
        preferences[cookieType] = selectedItem === 'true';
    }.bind(this));
    updateAnalyticsStorageConsent(true);
    const analyticsAccepted = preferences['analytics'];
    sendAnalyticsCustomEvent(analyticsAccepted, 'cookies');
    if (analyticsAccepted) {
        sendPageViewEvent();
    }
    else {
        updateAnalyticsStorageConsent(false);
    }
    updateClarityConsent(analyticsAccepted);
    setConsentCookie(preferences);
    // handle the corner case, where the user has selected their preference on the cookie page, whilst the banner is still open as they haven't previously selected their preference
    //todo: call hideBanner
    var banner = document.querySelector('[data-module="govuk-cookie-banner"]');
    banner.setAttribute('hidden', 'true');
    this.showSuccessNotification();
};
CookiesPage.prototype.showUserPreference = function ($cookieFormFieldset, preferences) {
    var cookieType = this.getCookieType($cookieFormFieldset);
    var preference = false;
    if (cookieType && preferences && preferences[cookieType] !== undefined) {
        preference = preferences[cookieType];
    }
    var radioValue = preference ? 'true' : 'false';
    var radio = $cookieFormFieldset.querySelector('input[name=' + cookieType + '][value=' + radioValue + ']');
    radio.checked = true;
};
CookiesPage.prototype.showSuccessNotification = function () {
    this.$successNotification.removeAttribute('hidden');
    // if the user's come to the cookies page through the link in the cookie banner, show a link to take them back to the page they came from
    //todo: use full url?
    var referrer = document.referrer ? new URL(document.referrer).pathname : false;
    if (referrer && referrer !== document.location.pathname) {
        this.$successLink.href = referrer;
        this.$successLink.removeAttribute('hidden');
    }
    else {
        this.$successLink.setAttribute('hidden', 'true');
    }
    // Set tabindex to -1 to make the element focusable with JavaScript.
    // GOV.UK Frontend will remove the tabindex on blur as the component doesn't
    // need to be focusable after the user has read the text.
    if (!this.$successNotification.getAttribute('tabindex')) {
        this.$successNotification.setAttribute('tabindex', '-1');
    }
    this.$successNotification.focus();
    // scroll to the top of the page
    window.scrollTo(0, 0);
};
CookiesPage.prototype.getCookieType = function ($cookieFormFieldset) {
    return $cookieFormFieldset.id;
};
export default CookiesPage;

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImNvbXBvbmVudHMvY29va2llcy1wYWdlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBLE9BQU8sRUFBRSxnQkFBZ0IsRUFBRSxnQkFBZ0IsRUFBaUIsTUFBTSxvQkFBb0IsQ0FBQTtBQUN0RixPQUFPLEVBQUUsZUFBZSxFQUFFLE1BQU0sV0FBVyxDQUFBO0FBQzNDLE9BQU8sRUFBRSxpQkFBaUIsRUFBRSx3QkFBd0IsRUFBRSw2QkFBNkIsRUFBRSxNQUFNLGFBQWEsQ0FBQTtBQUN4RyxPQUFPLEVBQUUsYUFBYSxJQUFJLG9CQUFvQixFQUFFLE1BQU0sV0FBVyxDQUFDO0FBRWxFLFNBQVMsV0FBVyxDQUFDLE9BQU87SUFDeEIsSUFBSSxDQUFDLE9BQU8sR0FBRyxPQUFPLENBQUE7QUFDMUIsQ0FBQztBQUVELFdBQVcsQ0FBQyxTQUFTLENBQUMsSUFBSSxHQUFHO0lBQ3pCLElBQUksQ0FBQyxXQUFXLEdBQUcsSUFBSSxDQUFDLE9BQU8sQ0FBQTtJQUUvQixJQUFJLENBQUMsSUFBSSxDQUFDLFdBQVcsRUFBRSxDQUFDO1FBQ3BCLE9BQU07SUFDVixDQUFDO0lBRUQsSUFBSSxDQUFDLFdBQVcsR0FBRyxJQUFJLENBQUMsV0FBVyxDQUFDLGFBQWEsQ0FBQyx1QkFBdUIsQ0FBQyxDQUFBO0lBQzFFLElBQUksQ0FBQyxvQkFBb0IsR0FBRyxJQUFJLENBQUMsV0FBVyxDQUFDLGdCQUFnQixDQUFDLGdDQUFnQyxDQUFDLENBQUE7SUFDL0YsSUFBSSxDQUFDLG9CQUFvQixHQUFHLElBQUksQ0FBQyxXQUFXLENBQUMsYUFBYSxDQUFDLDBCQUEwQixDQUFDLENBQUE7SUFDdEYsSUFBSSxDQUFDLFlBQVksR0FBRyxJQUFJLENBQUMsV0FBVyxDQUFDLGFBQWEsQ0FBQywrQkFBK0IsQ0FBQyxDQUFBO0lBRW5GLGVBQWUsQ0FBQyxJQUFJLENBQUMsb0JBQW9CLEVBQUUsVUFBVSxtQkFBbUI7UUFDcEUsSUFBSSxDQUFDLGtCQUFrQixDQUFDLG1CQUFtQixFQUFFLGdC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QVEsQ0FBQyxRQUFRLENBQUMsQ0FBQyxRQUFRLENBQUMsQ0FBQyxDQUFDLEtBQUssQ0FBQztJQUMvRSxJQUFJLFFBQVEsSUFBSSxRQUFRLEtBQUssUUFBUSxDQUFDLFFBQVEsQ0FBQyxRQUFRLEVBQUUsQ0FBQztRQUN0RCxJQUFJLENBQUMsWUFBWSxDQUFDLElBQUksR0FBRyxRQUFRLENBQUM7UUFDbEMsSUFBSSxDQUFDLFlBQVksQ0FBQyxlQUFlLENBQUMsUUFBUSxDQUFDLENBQUE7SUFDL0MsQ0FBQztTQUFNLENBQUM7UUFDSixJQUFJLENBQUMsWUFBWSxDQUFDLFlBQVksQ0FBQyxRQUFRLEVBQUUsTUFBTSxDQUFDLENBQUM7SUFDckQsQ0FBQztJQUVELG9FQUFvRTtJQUNwRSw0RUFBNEU7SUFDNUUseURBQXlEO0lBQ3pELElBQUksQ0FBQyxJQUFJLENBQUMsb0JBQW9CLENBQUMsWUFBWSxDQUFDLFVBQVUsQ0FBQyxFQUFFLENBQUM7UUFDdEQsSUFBSSxDQUFDLG9CQUFvQixDQUFDLFlBQVksQ0FBQyxVQUFVLEVBQUUsSUFBSSxDQUFDLENBQUE7SUFDNUQsQ0FBQztJQUVELElBQUksQ0FBQyxvQkFBb0IsQ0FBQyxLQUFLLEVBQUUsQ0FBQTtJQUVqQyxnQ0FBZ0M7SUFDaEMsTUFBTSxDQUFDLFFBQVEsQ0FBQyxDQUFDLEVBQUUsQ0FBQyxDQUFDLENBQUE7QUFDekIsQ0FBQyxDQUFBO0FBRUQsV0FBVyxDQUFDLFNBQVMsQ0FBQyxhQUFhLEdBQUcsVUFBVSxtQkFBbUI7SUFDL0QsT0FBTyxtQkFBbUIsQ0FBQyxFQUFFLENBQUE7QUFDakMsQ0FBQyxDQUFBO0FBRUQsZUFBZSxXQUFXLENBQUEiLCJmaWxlIjoiY29tcG9uZW50cy9jb29raWVzLXBhZ2UuanMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBnZXRDb25zZW50Q29va2llLCBzZXRDb25zZW50Q29va2llLCBDb25zZW50Q29va2llIH0gZnJvbSAnLi9jb29raWUtZnVuY3Rpb25zJ1xuaW1wb3J0IHsgbm9kZUxpc3RGb3JFYWNoIH0gZnJvbSAnLi9oZWxwZXJzJ1xuaW1wb3J0IHsgc2VuZFBhZ2VWaWV3RXZlbnQsIHNlbmRBbmFseXRpY3NDdXN0b21FdmVudCwgdXBkYXRlQW5hbHl0aWNzU3RvcmFnZUNvbnNlbnQgfSBmcm9tICcuL2FuYWx5dGljcydcbmltcG9ydCB7IHVwZGF0ZUNvbnNlbnQgYXMgdXBkYXRlQ2xhcml0eUNvbnNlbnQgfSBmcm9tIFwiLi9jbGFyaXR5XCI7XG5cbmZ1bmN0aW9uIENvb2tpZXNQYWdlKCRtb2R1bGUpIHtcbiAgICB0aGlzLiRtb2R1bGUgPSAkbW9kdWxlXG59XG5cbkNvb2tpZXNQYWdlLnByb3RvdHlwZS5pbml0ID0gZnVuY3Rpb24gKCkge1xuICAgIHRoaXMuJGNvb2tpZVBhZ2UgPSB0aGlzLiRtb2R1bGVcblxuICAgIGlmICghdGhpcy4kY29va2llUGFnZSkge1xuICAgICAgICByZXR1cm5cbiAgICB9XG5cbiAgICB0aGlzLiRjb29raWVGb3JtID0gdGhpcy4kY29va2llUGFnZS5xdWVyeVNlbGVjdG9yKCcuanMtY29va2llcy1wYWdlLWZvcm0nKVxuICAgIHRoaXMuJGNvb2tpZUZvcm1GaWVsZHNldHMgPSB0aGlzLiRjb29raWVGb3JtLnF1ZXJ5U2VsZWN0b3JBbGwoJy5qcy1jb29raWVzLXBhZ2UtZm9ybS1maWVsZHNldCcpXG4gICAgdGhpcy4kc3VjY2Vzc05vdGlmaWNhdGlvbiA9IHRoaXMuJGNvb2tpZVBhZ2UucXVlcnlTZWxlY3RvcignLmpzLWNvb2tpZXMtcGFnZS1zdWNjZXNzJylcbiAgICB0aGlzLiRzdWNjZXNzTGluayA9IHRoaXMuJGNvb2tpZVBhZ2UucXVlcnlTZWxlY3RvcignLmpzLWNvb2tpZXMtcGFnZS1zdWNjZXNzLWxpbmsnKVxuXG4gICAgbm9kZUxpc3RGb3JFYWNoKHRoaXMuJGNvb2tpZUZvcm1GaWVsZHNldHMsIGZ1bmN0aW9uICgkY29va2llRm9ybUZpZWxkc2V0KSB7XG4gICAgICAgIHRoaXMuc2hvd1VzZXJQcmVmZXJlbmNlKCRjb29raWVGb3JtRmllbGRzZXQsIGdldENvbnNlbnRDb29raWUoKSlcbi8qICAgICAgICAkY29va2llRm9ybUZpZWxkc2V0LnJlbW92ZUF0dHJpYnV0ZSgnaGlkZGVuJykqL1xuICAgIH0uYmluZCh0aGlzKSlcblxuICAgIC8vIFNob3cgc3VibWl0IGJ1dHRvblxuICAgIC8vdGhpcy4kY29va2llRm9ybS5xdWVyeVNlbGVjdG9yKCcuanMtY29va2llcy1mb3JtLWJ1dHRvbicpLnJlbW92ZUF0dHJpYnV0ZSgnaGlkZGVuJylcblxuICAgIHRoaXMuJGNvb2tpZUZvcm0uYWRkRXZlbnRMaXN0ZW5lcignc3VibWl0JywgdGhpcy5zYXZlUHJlZmVyZW5jZXMuYmluZCh0aGlzKSlcbn1cblxuQ29va2llc1BhZ2UucHJvdG90eXBlLnNhdmVQcmVmZXJlbmNlcyA9IGZ1bmN0aW9uIChldmVudCkge1xuICAgIC8vIFN0b3AgZGVmYXVsdCBmb3JtIHN1Ym1pc3Npb24gYmVoYXZpb3VyXG4gICAgZXZlbnQucHJldmVudERlZmF1bHQoKTtcblxuICAgIHZhciBwcmVmZXJlbmNlczogQ29uc2VudENvb2tpZSA9IHt9XG5cbiAgICBub2RlTGlzdEZvckVhY2godGhpcy4kY29va2llRm9ybUZpZWxkc2V0cyxcbiAgICAgICAgZnVuY3Rpb24oJGNvb2tpZUZvcm1GaWVsZHNldCkge1xuICAgICAgICAgICAgdmFyIGNvb2tpZVR5cGUgPSB0aGlzLmdldENvb2tpZVR5cGUoJGNvb2tpZUZvcm1GaWVsZHNldClcbiAgICAgICAgICAgIHZhciBzZWxlY3RlZEl0ZW0gPSAkY29va2llRm9ybUZpZWxkc2V0LnF1ZXJ5U2VsZWN0b3IoJ2lucHV0W25hbWU9JyArIGNvb2tpZVR5cGUgKyAnXTpjaGVja2VkJykudmFsdWVcblxuICAgICAgICAgICAgcHJlZmVyZW5jZXNbY29va2llVHlwZV0gPSBzZWxlY3RlZEl0ZW0gPT09ICd0cnVlJ1xuICAgICAgICB9LmJpbmQodGhpcykpO1xuXG4gICAgdXBkYXRlQW5hbHl0aWNzU3RvcmFnZUNvbnNlbnQodHJ1ZSk7XG4gICAgY29uc3QgYW5hbHl0aWNzQWNjZXB0ZWQgPSBwcmVmZXJlbmNlc1snYW5hbHl0aWNzJ107XG4gICAgc2VuZEFuYWx5dGljc0N1c3RvbUV2ZW50KGFuYWx5dGljc0FjY2VwdGVkLCAnY29va2llcycpO1xuXG4gICAgaWYgKGFuYWx5dGljc0FjY2VwdGVkKSB7XG4gICAgICAgIHNlbmRQYWdlVmlld0V2ZW50KCk7XG4gICAgfSBlbHNlIHtcbiAgICAgICAgdXBkYXRlQW5hbHl0aWNzU3RvcmFnZUNvbnNlbnQoZmFsc2UpO1xuICAgIH1cblxuICAgIHVwZGF0ZUNsYXJpdHlDb25zZW50KGFuYWx5dGljc0FjY2VwdGVkKTtcbiAgICBzZXRDb25zZW50Q29va2llKHByZWZlcmVuY2VzKTtcblxuICAgIC8vIGhhbmRsZSB0aGUgY29ybmVyIGNhc2UsIHdoZXJlIHRoZSB1c2VyIGhhcyBzZWxlY3RlZCB0aGVpciBwcmVmZXJlbmNlIG9uIHRoZSBjb29raWUgcGFnZSwgd2hpbHN0IHRoZSBiYW5uZXIgaXMgc3RpbGwgb3BlbiBhcyB0aGV5IGhhdmVuJ3QgcHJldmlvdXNseSBzZWxlY3RlZCB0aGVpciBwcmVmZXJlbmNlXG4gICAgLy90b2RvOiBjYWxsIGhpZGVCYW5uZXJcbiAgICB2YXIgYmFubmVyID0gZG9jdW1lbnQucXVlcnlTZWxlY3RvcignW2RhdGEtbW9kdWxlPVwiZ292dWstY29va2llLWJhbm5lclwiXScpO1xuICAgIGJhbm5lci5zZXRBdHRyaWJ1dGUoJ2hpZGRlbicsICd0cnVlJyk7XG4gICAgXG4gICAgdGhpcy5zaG93U3VjY2Vzc05vdGlmaWNhdGlvbigpXG59XG5cbkNvb2tpZXNQYWdlLnByb3RvdHlwZS5zaG93VXNlclByZWZlcmVuY2UgPSBmdW5jdGlvbiAoJGNvb2tpZUZvcm1GaWVsZHNldCwgcHJlZmVyZW5jZXMpIHtcbiAgICB2YXIgY29va2llVHlwZSA9IHRoaXMuZ2V0Q29va2llVHlwZSgkY29va2llRm9ybUZpZWxkc2V0KVxuICAgIHZhciBwcmVmZXJlbmNlID0gZmFsc2VcblxuICAgIGlmIChjb29raWVUeXBlICYmIHByZWZlcmVuY2VzICYmIHByZWZlcmVuY2VzW2Nvb2tpZVR5cGVdICE9PSB1bmRlZmluZWQpIHtcbiAgICAgICAgcHJlZmVyZW5jZSA9IHByZWZlcmVuY2VzW2Nvb2tpZVR5cGVdXG4gICAgfVxuXG4gICAgdmFyIHJhZGlvVmFsdWUgPSBwcmVmZXJlbmNlID8gJ3RydWUnIDogJ2ZhbHNlJ1xuICAgIHZhciByYWRpbyA9ICRjb29raWVGb3JtRmllbGRzZXQucXVlcnlTZWxlY3RvcignaW5wdXRbbmFtZT0nICsgY29va2llVHlwZSArICddW3ZhbHVlPScgKyByYWRpb1ZhbHVlICsgJ10nKVxuICAgIHJhZGlvLmNoZWNrZWQgPSB0cnVlXG59XG5cbkNvb2tpZXNQYWdlLnByb3RvdHlwZS5zaG93U3VjY2Vzc05vdGlmaWNhdGlvbiA9IGZ1bmN0aW9uICgpIHtcbiAgICB0aGlzLiRzdWNjZXNzTm90aWZpY2F0aW9uLnJlbW92ZUF0dHJpYnV0ZSgnaGlkZGVuJylcblxuICAgIC8vIGlmIHRoZSB1c2VyJ3MgY29tZSB0byB0aGUgY29va2llcyBwYWdlIHRocm91Z2ggdGhlIGxpbmsgaW4gdGhlIGNvb2tpZSBiYW5uZXIsIHNob3cgYSBsaW5rIHRvIHRha2UgdGhlbSBiYWNrIHRvIHRoZSBwYWdlIHRoZXkgY2FtZSBmcm9tXG4gICAgLy90b2RvOiB1c2UgZnVsbCB1cmw/XG4gICAgdmFyIHJlZmVycmVyID0gZG9jdW1lbnQucmVmZXJyZXIgPyBuZXcgVVJMKGRvY3VtZW50LnJlZmVycmVyKS5wYXRobmFtZSA6IGZhbHNlO1xuICAgIGlmIChyZWZlcnJlciAmJiByZWZlcnJlciAhPT0gZG9jdW1lbnQubG9jYXRpb24ucGF0aG5hbWUpIHtcbiAgICAgICAgdGhpcy4kc3VjY2Vzc0xpbmsuaHJlZiA9IHJlZmVycmVyO1xuICAgICAgICB0aGlzLiRzdWNjZXNzTGluay5yZW1vdmVBdHRyaWJ1dGUoJ2hpZGRlbicpXG4gICAgfSBlbHNlIHtcbiAgICAgICAgdGhpcy4kc3VjY2Vzc0xpbmsuc2V0QXR0cmlidXRlKCdoaWRkZW4nLCAndHJ1ZScpO1xuICAgIH1cblxuICAgIC8vIFNldCB0YWJpbmRleCB0byAtMSB0byBtYWtlIHRoZSBlbGVtZW50IGZvY3VzYWJsZSB3aXRoIEphdmFTY3JpcHQuXG4gICAgLy8gR09WLlVLIEZyb250ZW5kIHdpbGwgcmVtb3ZlIHRoZSB0YWJpbmRleCBvbiBibHVyIGFzIHRoZSBjb21wb25lbnQgZG9lc24ndFxuICAgIC8vIG5lZWQgdG8gYmUgZm9jdXNhYmxlIGFmdGVyIHRoZSB1c2VyIGhhcyByZWFkIHRoZSB0ZXh0LlxuICAgIGlmICghdGhpcy4kc3VjY2Vzc05vdGlmaWNhdGlvbi5nZXRBdHRyaWJ1dGUoJ3RhYmluZGV4JykpIHtcbiAgICAgICAgdGhpcy4kc3VjY2Vzc05vdGlmaWNhdGlvbi5zZXRBdHRyaWJ1dGUoJ3RhYmluZGV4JywgJy0xJylcbiAgICB9XG5cbiAgICB0aGlzLiRzdWNjZXNzTm90aWZpY2F0aW9uLmZvY3VzKClcblxuICAgIC8vIHNjcm9sbCB0byB0aGUgdG9wIG9mIHRoZSBwYWdlXG4gICAgd2luZG93LnNjcm9sbFRvKDAsIDApXG59XG5cbkNvb2tpZXNQYWdlLnByb3RvdHlwZS5nZXRDb29raWVUeXBlID0gZnVuY3Rpb24gKCRjb29raWVGb3JtRmllbGRzZXQpIHtcbiAgICByZXR1cm4gJGNvb2tpZUZvcm1GaWVsZHNldC5pZFxufVxuXG5leHBvcnQgZGVmYXVsdCBDb29raWVzUGFnZSJdfQ==
