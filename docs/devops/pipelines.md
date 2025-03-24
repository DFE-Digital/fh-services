# Pipelines

## Build and Test

The build and test workflow builds the code, runs unit tests - all in a parallel
matrix. This is to improve the feedback loop for issues. This is triggered by PR
changes to main or a release branch.

Under the hood, it uses a template workflow that installs a report tool (Liquid
Test Reports), optionally configures the Linux instance with support for
Spatialite - a framework used by Sqlite to support our geo-spatial function.
Then it performs the standard dotnet build and test tasks and generates a test
report.

## Re-seeding Databases

We have a set of scripts to reseed the databases in a number of environments. There are scripts for:

* Test1
* Test2
* Pre-production

The current deployment workflow, is enabled to reseed for test and
pre-production only. This will be extended to cover  development too. Also the
current process is integrated into the deploy.yml file. Although the appropriate
if statements are in place, along with a checkbox to disable reseeding, this
really needs to be abstracted into its own workflow. This will provide the
following benefits:

* Deterministic reseeding. This is because before the database can be reseeded, it will need to be reset. Then migrations are run to bring the databases into their current state, followed by reseeding with the data required
* Safety against accidental production destruction. We **do not** want to accidentally run the reset step of the databases against production. Currently we have a separate workflow, to mitigate this which only applies the re-seeding against the test environment.
* We can choose when to reseed. There are scenarios where a tester might want to leave an environment in a certain state whilst testing a ticket and wait for a fix/change and redeploy.

## SonarCloud

### Administration & Access

Link to our SonarCloud Instance: <https://sonarcloud.io/project/overview?id=DFE-Digital_fh-services>

The SonarCloud instance is part of the DfE Organisation, linked here: <https://sonarcloud.io/organizations/dfe-digital/projects>

The list of admins of the DfE Organisation is here: <https://sonarcloud.io/organizations/dfe-digital/members>

We **do not** get administrator access to our project, only the admins listed on the above link have the power to configure projects at the SonarCloud level. If we need to make a fundamental change to our SonarCloud project we have to escalate it with either one of the admins listed (reachable via Slack) or via a ServiceNow ticket. To do it via ServiceNow you need to raise a GitHub request as we can only get SonarCloud changes related to our repo config (i.e., threshold changes for code coverage, duplication etc are off limits as they are organisation level). The quick link for this is: <https://dfe.service-now.com.mcas.ms/serviceportal?id=sc_cat_item&sys_id=0aacf3a81ba52110b192ec69b04bcb14> - select “Other” for the request type and give as much detail as you can think of in the request.

### Configuration

I have already gotten automatic analysis switched off, this allows us to analyse our code using CI pipelines. You will find our current implementation in .github/workflows/sonarcloud.yml in the repository. It is important that the step with dotnet-sonarscanner begin happens before both the build and test steps, and that the dotnet-sonarscanner end step happens afterwards.

We can configure it in essentially three ways:

* In the CI pipeline, you can specify global glob pattern exclusions from analysis and/or code coverage, or exclude rules that we justifiably don’t need.
* At the .csproj level, you can exclude projects with MSBUILD → <https://docs.sonarsource.com/sonarqube-cloud/advanced-setup/ci-based-analysis/sonarscanner-for-dotnet/configuring/#excluding-projects-from-analysis>
* On a per-class level, you can add [ExcludeFromCodeCoverage] above the class definition to exclude individual classes, or above class members within them (variables, functions) to exclude those specific members.

### Output

As you see on the dashboard, it reports on a whole host of things. To see the state of main, go to “Main Branch” and “Overall Code” (quick link: <https://sonarcloud.io/summary/overall?id=DFE-Digital_fh-services&branch=main>). It will tell us how many code smells we have - this is code which follows bad practice, bugs and vulnerabilities, and can range in severity from: Info, Low, Medium, High, Blocker. They are categorised into things like code security, maintainability and reliability issues. The “Issues” tab on the branch of interest will sort everything it detects for you.

It will also tell us our code coverage. We use Coverlet so it needs to be installed on each project in the repository.

It will also tell us our code duplication percentage.

New code is analysed relative to the previous analysis of either 1. the same branch (e.g., updating main or a release branch) or 2. the branch you are merging into (currently this will be release-\* relative to main)

We currently run SonarCloud in a “monitoring mode” of sorts. This means we don’t run it at the Pull Request level and only on Push to main or release-\*. This gives us overall views of our long running branches without blocking PRs, as we need to bring the branches (namely main) within the passing thresholds before using it as a quality gate.

## Deployment

Please see [Deployment guide](https://dfedigital.atlassian.net/wiki/spaces/FHGUW/pages/4329930754/Deployment+guide) for information.

## GitHub Workflows

We use GitHub to build, deploy, provision and analyse our application. Each of the 'triggered' workflows uses a combination of callable workflows and actions to carry out the pipeline actions.

Use the [GitHub Actions UI](https://github.com/DFE-Digital/fh-services/actions) workflow list to inspect workflow structures and relationships.