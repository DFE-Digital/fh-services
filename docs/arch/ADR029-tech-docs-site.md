
# ADR029 - Implement technical documentation site

- **Status**: Adopted but not yet implemented
- **Date**: 2025-03-05
- **Author**: Aaron Yarborough

## Decision

Implement option 4, which is a DfE-skinned version of the tech-docs-template outlined in Option 2.

## Context

We are in the process of transferring technical documentation from Confluence, which is accessible only to DfE users, to GitHub. This transition aims to enhance accessibility for others to understand and build upon our software, for which technical documentation plays an important role. Other projects within the VC&F portfolio have already adopted tools like static site generators to improve document viewing and searchability through a user-friendly interface. The original requirement came off the back of an internal Hippo review, as well as advice from the portfolio architect.

## Options considered

1. Continue using GitHub's repository explorer
2. Use DfE's [tech-docs-template](https://github.com/DFE-Digital/tech-docs-template)
3. Use GovUK One Login's [tech-docs](https://github.com/govuk-one-login/tech-docs)
4. (SELECTED) Copy [Care Leaver's approach using MKDocs](https://github.com/DFE-Digital/care-leavers/tree/main/resources/tech_docs_template)


## Consequences

### Option 1 - Continue using GitHub's repository explorer

* Requires no additional development or hosting costs, because no additional components/architecture changes would be required.
* The GitHub UI presents the user with technical language and components they're not necessarily used to (technical README, various tabs at the top of the UI, license information, etc.), and does not make sense for someone who purely wants to read technical documentation
* The GitHub search function defaults to searching the full repo, meaning code search results will be shown and not just document search results, potentially further confusing the users 

### Option 2 - Use DfE's [tech-docs-template](https://github.com/DFE-Digital/tech-docs-template)

* Requires setup of GitHub pages, but would not require any additional components or architecture changes, or any associated hosting costs.
* Is a static site generator, meaning we can use GitHub pages to host the output HTML files.
* Is a solution recommended by the DfE.
* Doesn't look like other DfE technical docs, as the DfE design kit isn't used, though this doesn't seem to be a major consideration given received advice.

### Option 3 - Use GovUK One Login's [tech-docs](https://github.com/govuk-one-login/tech-docs)

* Requires hosting a web server with Ruby installed. This would require development work, new architectural components, and would require regular maintenance.
* Uses official Gov UK and DfE design kit elements, bringing it in line with other GDS documentation sites.

### Option 4 - Copy [Care Leavers' approach using MKDocs](https://github.com/DFE-Digital/care-leavers/tree/main/resources/tech_docs_template)

* Requires setup of GitHub pages, but would not require any additional components or architecture changes, or any associated hosting costs.
* Is themed to look like other Government technical documentation sites.
* Is a static site generator, meaning we can use GitHub pages to host the output HTML files.
* Isn't (yet) an officially recommended DfE approach, though it uses the same underlying technology (MKDOcs) as Option 3 - the only difference is the theme to make it look like other DfE technical docs.
* Is being used already on Care Leavers, meaning we're more in-line with other projects on the VC&F portfolio.

## Advice

**Harry Young (Care Leavers - Software Engineer)**

Option 4 could be enhanced later with the DfE design kit to closely resemble Option 3's formal DfE documentation style. The solution was adapted from an MOJ MkDocs tech docs template on GitHub, with MOJ references stripped out. Hosting a Ruby site adds unnecessary complexity, making MkDocs a simpler choice.

**Joshua Taylor (Family Hubs - Technical Architect)**

Sees value in aligning with the portfolio but prioritizes practicality. Option 2 (using a static site generator and free GitHub Pages) is favoured over Option 3 (server-side/hosted app) due to cost savings and simplicity, despite potential trade-offs in search functionality and strict DfE branding adherence. Hesitates to invest effort in tailoring to DfE's style when not necessarily required but acknowledges its benefits. Option 4 is considered if stakeholders prioritize visual consistency with GDS documentation. Overall, Option 2 is preferred unless stakeholders specifically push for another direction.

**Ben Vandersteen (Lead Technical Architect - VC&F portfolio)**

Views the necessity of hosting and managing a server (option 2) as a drawback due to associated costs and overhead; prefers a simpler solution. Happy with the search functionality and overall styling of Option 4, and prefers this over option 2. As a side: considering merging changes from Option 4 into the repository for Option 2 to gain ownership of the tech-docs-template repository and has started the ball rolling on this.