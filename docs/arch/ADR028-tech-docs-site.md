
# ADR028 - Implement technical documentation site

- **Status**: Draft
- **Date**: 2025-03-05
- **Author**: Aaron Yarborough

## Decision

...

## Context

We are in the process of transferring technical documentation from Confluence, which is accessible only to DfE users, to GitHub. This transition aims to enhance accessibility for others to understand and build upon our software, for which technical documentation plays an important role. Other projects within the VC&F portfolio have already adopted tools like static site generators to improve document viewing and searchability through a user-friendly interface. The original requirement came off the back of an internal Hippo review, as well as advice from the portfolio architect.

## Options considered

1. Continue using GitHub's repository explorer
2. Use DfE's [tech-docs-template](https://github.com/DFE-Digital/tech-docs-template)
3. Use GovUK One Login's [tech-docs](https://github.com/govuk-one-login/tech-docs)
4. Copy [Care Leaver's approach using MKDocs](https://github.com/DFE-Digital/care-leavers/tree/main/resources/tech_docs_template)


## Consequences

### Option 1 - Continue using GitHub's repository explorer

* Requires no additional components/architecture change
* The GitHub UI presents the user with technical language and components they're not necessarily used to (technical README, various tabs at the top of the UI, license information, etc.), and does not make sense for someone who purely wants to read technical documentation
* The GitHub search function defaults to searching the full repo, meaning code search results will be shown and not just document search results, potentially further confusing the users 

### Option 2 - Use DfE's [tech-docs-template](https://github.com/DFE-Digital/tech-docs-template)

* Requires additional components/architecture
* Is a static site generator, meaning we can use GitHub pages to host the output HTML files
* Is a solution recommended by the DfE
* Doesn't look like other DfE technical docs, as the DfE design kit isn't used

### Option 3 - Use GovUK One Login's [tech-docs](https://github.com/govuk-one-login/tech-docs)

* Requires additional components/architecture

### Option 4 - Copy [Care Leaver's approach using MKDocs](https://github.com/DFE-Digital/care-leavers/tree/main/resources/tech_docs_template)

* Requires additional components/architecture
* Is themed to look like other Government technical documentation sites
* Is a static site generator, meaning we can use GitHub pages to host the output HTML files
* Isn't (yet) an officially recommended DfE approach, though it uses the same underlying technology (MKDOcs) as Option 3 - the only difference is the theme to make it look like other DfE technical docs
* Is being used already on Care Leavers, meaning we're more in-line with other projects on the VC&F portfolio

## Advice

**Harry Young (Care Leavers - Software Engineer)**

Option 4 could be enhanced later with the DfE design kit to closely resemble Option 3's formal DfE documentation style. The solution was adapted from an MOJ MkDocs tech docs template on GitHub, with MOJ references stripped out. Hosting a Ruby site adds unnecessary complexity, making MkDocs a simpler choice.

**Joshua Taylor (Family Hubs - Technical Architect)**

Sees value in aligning with the portfolio but prioritizes practicality. Option 2 (using a static site generator and free GitHub Pages) is favoured over Option 3 (server-side/hosted app) due to cost savings and simplicity, despite potential trade-offs in search functionality and strict DfE branding adherence. Hesitates to invest effort in tailoring to DfE's style when not necessarily required but acknowledges its benefits. Option 4 is considered if stakeholders prioritize visual consistency with GDS documentation. Overall, Option 2 is preferred unless stakeholders specifically push for another direction.

**Ben Vandersteen (Lead Technical Architect - VC&F portfolio)**
TODO.