#Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "= 4.19.0"
    }
  }
}

provider "azurerm" {
  subscription_id = var.subscription_id
  features {
    key_vault {
      purge_soft_deleted_keys_on_destroy = true
      recover_soft_deleted_keys = false
    }
  }
}

# This is overriden by the pipeline
terraform {
  backend "azurerm" {
    resource_group_name = "s181d01-familyhubs-terraform"
    storage_account_name = "s181d01safhterraform"
    container_name = "s181d01appserviceterraform"
    key = "s181d01appserviceterraform/s181d01appservicetf.tfstate"
  }
}
