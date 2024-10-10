
resource "azurerm_storage_account" "storage_account" {
  name = var.storage_account_name
  resource_group_name = var.resource_group_name
  location = var.location
  account_tier = "Standard"
  account_kind = "StorageV2"
  access_tier = "Hot"
  min_tls_version = "TLS1_2"
  is_hns_enabled = false
  public_network_access_enabled = true
  account_replication_type = "LRS"
  infrastructure_encryption_enabled = true
  blob_properties {
    versioning_enabled = true
    change_feed_enabled  = true
    delete_retention_policy {
      days = 7
    }
    container_delete_retention_policy {
      days = 7
    }
  }
  tags = var.tags
}