# Family Hubs Technical Documentation

This directory contains source code and markdown documentation used to generate the Family Hubs technical documentation static site. This codebases uses the [DFE-Digital/tech-docs-template](https://github.com/DFE-Digital/tech-docs-template) repo as a base.

# Getting set up

**Note:** This project uses `pyenv`. If you wish to skip the below steps, ensure you have a version of python installed that matches the version defined in `.python-version` file in this directory.

1. Install `pyenv` (see [pyenv's 'Getting pyenv' documentation](https://github.com/pyenv/pyenv?tab=readme-ov-file#a-getting-pyenv))
2. Configure your shell environment to use `pyenv` (see [pyenv's 'Set up your shell environment for Pyenv'](https://github.com/pyenv/pyenv?tab=readme-ov-file#b-set-up-your-shell-environment-for-pyenv))
3. Run `python --version` and ensure the version matches that defined in the `.python-version` file in this directory
4. Install dependencies:
    ```sh
    $ pip install mkdocs-material
    ```
5. Run the mkdocs server to view the documentation site
    ```sh
    $ mkdocs serve
    ```

# Adding and editing documentation

To add or edit documentation, create or edit a file under the `docs/` directory.

## Adding a new ADR

[add guidance on running the ADR script]