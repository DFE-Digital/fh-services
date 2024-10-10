variable "location" {
  type = string
  description = "The Azure region the services are deployed in."
}

variable "resource_group_name" {
  type = string
  description = "The name of the resource group the services reside in."
}

variable "storage_account_name" {
  type = string
  description = "Name of the storage account."
}

variable "tags" {
  type = map(string)
  description = "Tags to apply to the resource."
  # Defines the structure but the values are provided by the caller
  default = {
    "Parent Business" = ""
    "Service Offering" = ""
    "Portfolio" = ""
    "Service Line" = ""
    "Service" = ""
    "Product" = ""
    "Environment" = ""
  }
}

output "storage_account_name" {
  value = azurerm_storage_account.storage_account.name
  description = "The name of the created storage account."
}

output "storage_account_primary_access_key" {
  value = azurerm_storage_account.storage_account.primary_access_key
  description = "The primary access key for the created storage account."
}