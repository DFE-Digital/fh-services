export {};

declare const accessibleAutocomplete: any;
declare global {
    interface Window {
        fhgov: any;
    }
}

function fhgov() {
    this.init = function () {
        restoreConditionalInputs();
        enhanceAccessibleAutocomplete();
    };

    let restoreConditionalInputs = function () {
        const element = document.querySelector("[data-conditional-active]");
        if (element instanceof HTMLElement) {
            element.click()
        }

    }

    //todo: replace these with the new full page autocomplete component

    let enhanceAccessibleAutocomplete = function () {

        //WhichLocalAuthority.cshtml && AddOrganisationWhichLocalAuthority.cshtml
        const modelLaOrganisationName = document.getElementById('modelLaOrganisationName') as HTMLInputElement;
        if (modelLaOrganisationName) {
            accessibleAutocomplete.enhanceSelectElement({
                defaultValue: modelLaOrganisationName.value,
                name: 'LaOrganisationName',
                selectElement: document.querySelector('#LaOrganisationName')
            });
        }

        //WhichVcsOrganisation.cshtml
        const modelVcsOrganisationName = document.getElementById('modelVcsOrganisationName') as HTMLInputElement;
        if (modelVcsOrganisationName) {
            accessibleAutocomplete.enhanceSelectElement({
                defaultValue: modelVcsOrganisationName.value,
                name: 'VcsOrganisationName',
                selectElement: document.querySelector('#VcsOrganisationName')
            })
        }
    }
}

window.fhgov = new fhgov();

document.addEventListener('DOMContentLoaded', function () {
    window.fhgov.init();
});

function setupLanguageAutocompleteWhenAddAnother(element: HTMLElement) {

    if (!(element instanceof HTMLElement)) {
        return;
    }

    const languageSelects = element.querySelectorAll("select[id^='language-']") as NodeListOf<HTMLSelectElement>;

    /*    console.log('enhancing ' + languageSelects.length + ' language selects');*/

    // work around accessible-autocomplete not handling errors or using standard govuk styling classes
    // there's a discussion about handling errors here...
    // https://github.com/alphagov/accessible-autocomplete/issues/428
    // but we've had to implement our own (hacky) solution by using MutationObserver
    // and adding extra classes (with custom css) to the input element.
    // we are observing the DOM for changes because enhanceSelectElement() ultimately
    // calls render in Peact, which schedules an update to the DOM, rather than immediately updating the DOM.
    // (if we forked the accessible-autocomplete component, we could use componentDidMount or useEffect instead).
    // we also observe any changes to the class attribute of the text input elements,
    // as any changes we make to the input element's class attribute will be overwritten by the component (on focus etc.).

    // I was going to either package up this code into an exported function to ease reuse and maintanence,
    // or fork the accessible-autocomplete preact component,
    // but someone is adding official support today (2024-01-12) so we should be able to remove this soon!
    // https://github.com/alphagov/accessible-autocomplete/pull/602

    //todo: fix aria-describedBy on the input too
    // see https://github.com/alphagov/accessible-autocomplete/issues/589

    const domObserver = new MutationObserver((mutationsList, observer) => {
        const childListMutation = mutationsList.some(mutation => mutation.type === 'childList' && mutation.addedNodes.length > 0);
        const attributesMutation = mutationsList.some(mutation => {
            if (mutation.type === 'attributes' && mutation.attributeName === 'class') {
                const targetElement = mutation.target as HTMLElement;
                return targetElement.tagName.toLowerCase() === 'input' && targetElement.getAttribute('type') === 'text';
            }
            return false;
        });

        //if (childListMutation) {
        //    console.log('childListMutation');
        //}

        //if (attributesMutation) {
        //    console.log('attributesMutation');
        //}

        if (childListMutation || attributesMutation) {
            /*todo: create list of input ids outside of observer? */
            languageSelects.forEach(function (select) {
                const input = document.getElementById(select.id.replace('-select', '')) as HTMLInputElement;

                // input should never be null now we're observing the DOM for changes, but we check it for extra safety
                if (!input) {
                    return;
                }

                const errorState = select.classList.contains('govuk-select--error');

                addGovUkClasses(input, errorState);
            });
        }
    });

    domObserver.observe(element, {childList: true, subtree: true, attributes: true});

    languageSelects.forEach(function (select) {
        accessibleAutocomplete.enhanceSelectElement({
            name: 'languageName',
            defaultValue: '',
            selectElement: select
        });
    });
}

function addGovUkClasses(input: HTMLInputElement, errorState: boolean) {
    if (!input.classList.contains('govuk-input')) {
        input.classList.add('govuk-input');
    }

    if (errorState && !input.classList.contains('govuk-input--error')) {
        input.classList.add('govuk-input--error');
    }
}

/**
 * This function is used to update the "Remove" button text in the "Add another" component
 * and update the label text for each item in the "Add another" component when adding a new language item to the DOM.
 *
 * NOTE: Adding of DOM elements is done in the familyhubs-frontend FamilyHubsFrontend.AddAnother.prototype list.
 * Doing this UI update here as it makes sense being that it's only for the language page.
 */
function handleUpdatingLanguageAddAnother() {
    function updateAllRemoveButtonText() {
        const items = document.querySelectorAll('.fh-add-another__item');

        items.forEach((item, index) => {
            const button = item.querySelector('.fh-add-another__remove-button') as HTMLButtonElement;
            if (button) {
                button.textContent = `Remove language ${(index + 1)}`;
            }
        });
    }

    function updateAllLanguageLabelText() {
        const items = document.querySelectorAll('.fh-add-another__item');

        items.forEach((item, index) => {
            const label = item.querySelector('label');
            if (label) {
                label.textContent = `Enter language ${(index + 1)}`;
            }
        });
    }

    // Observe DOM changes to react to new items being added/removed
    const observer = new MutationObserver((mutationsList, observer) => {
        for (const mutation of mutationsList) {
            const mixedNodes = Array.from(mutation.addedNodes).concat(Array.from(mutation.removedNodes));
            if (mutation.type === 'childList' && (mixedNodes.some(node => node.nodeName === 'FIELDSET'))) {
                
                updateAllLanguageLabelText();
                updateAllRemoveButtonText();
            }
        }
    });

    const container = document.getElementById('fh-add-another-id');
    if (container) {
        observer.observe(container, {childList: true, subtree: true});
    }
}

//todo: this is a hack - we want setupLanguageAutocompleteWhenAddAnother to be in the generated js file.
// if we export it, it includes the export keyword in the generated js file
// (but we use export in the other ts files, without the js containing export!)
// so as a workaround we call it where it no-ops
setupLanguageAutocompleteWhenAddAnother(null);
handleUpdatingLanguageAddAnother();
