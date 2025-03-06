import {By, PageElement} from '@serenity-js/web';

export const startButton = () =>
    PageElement
        .located(By.css("[data-testid='start-button']"))
        .describedAs('start Now Button');

export const postcodeSearchBox = () =>
    PageElement
        .located(By.id("postcode"))
        .describedAs('the Postcode searchbox');


export const postcodeSearchButton = () =>
    PageElement
        .located(By.css("[data-testid='button-search']"))
        .describedAs('the search button');

export const filterCategoryFamilySupportAccordion = () =>
    PageElement.located(By.css("[data-testid='accordion-category-Family']"));

export const filterSubCategoryDebtAndWelfareAdviceCheckbox = () =>
    PageElement.located(By.css("[data-testid='debt-and-welfare-advice']"));

export const filterSubCategoryMoneyBenefitsAndHousingCheckbox = () =>
    PageElement.located(By.css("[data-testid='money,-benefits-and-housing']"));

export const filterOnlyShowFreeServicesCheckbox = () =>
    PageElement.located(By.css("[data-testid='cost-free']"));

export const filterSearchWithinRadioButton = (miles: number) =>
    PageElement.located(By.css(`[data-testid='search-within-${miles}']`));

export const filterApplyFiltersButton = () =>
    PageElement.located(By.css("[data-test-id='submit-button']"));

export const filterDayAvailable = (dayCode: string) =>
    PageElement.located(By.css(`[data-testid='days-${dayCode}']`));

export const filterAgeRange = (indexNumber: string) =>
    PageElement.located(By.css(`[data-testid='age-$${indexNumber}']`));

export const filterLanguageList = () =>
    PageElement.located(By.css("[data-testid='select-language']"));

export const filterLanguage = (languageName: string) =>
    PageElement.located(By.css(`[data-testid='language-${languageName}']`));