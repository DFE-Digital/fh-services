
locals {

  resource_group_name = "${var.prefix}-familyhubs"
  
  # App Gateway configuration
  
  appgw_probe_referral_ui_name             = "fh-health-probe-referral-ui"
  appgw_rewrites_referral_ui_name          = "fh-appgw-referral-ui-rewrites"
  appgw_bep_referral_ui_name               = "fh-bep-referral-ui"
  appgw_backend_referral_ui_name           = "fh-backend-referral-ui"
  appgw_listener_http_referral_ui_name     = "fh-listener-http-referral-ui"
  appgw_listener_https_referral_ui_name    = "fh-listener-https-referral-ui"
  appgw_redirect_referral_ui_name = "fh-redirect-referral-ui"
  
  appgw_probe_sd_admin_ui_name = "fh-health-probe-sd-admin-ui"
  appgw_rewrites_sd_admin_ui_name = "fh-appgw-sd-admin-ui-rewrites"
  appgw_bep_sd_admin_ui_name = "fh-bep-sd-admin-ui"
  appgw_backend_sd_admin_ui_name = "fh-backend-sd-admin-ui"
  appgw_listener_http_sd_admin_ui_name = "fh-listener-http-sd-admin-ui"
  appgw_listener_https_sd_admin_ui_name = "fh-listener-https-sd-admin-ui"

  appgw_redirect_sd_admin_ui_name = "fh-redirect-sd-admin-ui"
  appgw_rewrites_sd_ui_name = "fh-appgw-sd-ui-rewrites"
  appgw_bep_sd_ui_name = "fh-bep-sd-ui"
  appgw_backend_sd_ui_name = "fh-backend-sd-ui"
  appgw_probe_sd_ui_name = "fh-health-probe-sd-ui"
  appgw_listener_http_sd_ui_name = "fh-listener-http-sd-ui"
  appgw_listener_https_sd_ui_name = "fh-listener-https-sd-ui"
  appgw_redirect_sd_ui_name = "fh-redirect-sd-ui"

  appgw_ssl_cert_sd_admin_ui_name= "sd-admin-ui-${lower(var.environment)}-cert"
  appgw_ssl_cert_sd_ui_name = "sd-ui-${lower(var.environment)}-cert"
  appgw_ssl_cert_referral_ui_name = "referral-ui-${lower(var.environment)}-cert"
  
  # Storage configuration
  
  account_kind = "StorageV2"
  account_tier = "Standard"
  is_hns_enabled  = false
  account_replication_type  = "LRS"
  access_tier = "Hot"
  min_tls_version = "TLS1_2"
  versioning_enabled = true
  change_feed_enabled = true
  delete_retention_policy_days = 7
  container_delete_retention_policy_days = 7
  public_network_access_enabled_storage = true
  infrastructure_encryption_enabled = true
  
  # Tags
  tags = {
    "Parent Business" = "Children's Care"
    "Service Offering" = "Growing Up Well"
    "Portfolio" = "Newly Onboarded"
    "Service Line" = "Newly Onboarded"
    "Service" = "Children's Care"
    "Product" = "Growing Up Well"
    "Environment" = var.environment
  }
}

# Application Gateways
resource "azurerm_application_gateway" "ref_ui_app_gateway" {
  name                = "${var.prefix}-fh-appgw-referral-ui"
  resource_group_name = local.resource_group_name
  location            = var.location
  tags = local.tags

  autoscale_configuration {
    min_capacity                = 1
    max_capacity                = 1
  }

  ssl_certificate {
    name     = "${var.prefix}-${local.appgw_ssl_cert_referral_ui_name}"
    data     = var.ssl_cert_path_referral_ui
    password = var.certificate_password
  }

  probe {
    name                        = "${var.prefix}-${local.appgw_probe_referral_ui_name}"
    protocol                    = "Http"
    interval                    = "30"
    timeout                     = "30"
    unhealthy_threshold         = "3"
    pick_host_name_from_backend_http_settings = true
    path                        = "/api/health"
    match {
      status_code               = [ "200-399" ]
    }
  }

  ssl_policy {
    policy_type           = "Predefined"
    policy_name           = "AppGwSslPolicy20220101"
  }

  rewrite_rule_set {
     name                 = "${var.prefix}-${local.appgw_rewrites_referral_ui_name}"
     rewrite_rule {
        name              = "NewRewrite"
        rule_sequence     = "100"       
     response_header_configuration {
       header_name        = "Server"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "X-Powered-By"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "Strict-Transport-Security"
       header_value       = "max-age=31536000; includeSubDomains; preload"
      }
     request_header_configuration {
       header_name        = "X-Forwarded-Host"
       header_value       = var.connect_domain
      }
     }
   }

  backend_address_pool {
      name                = "${var.prefix}-${local.appgw_bep_referral_ui_name}"
      ip_addresses        = [var.private_endpoint_ip_address.referral_ui]
  }

  backend_http_settings {
    name                = "${var.prefix}-${local.appgw_backend_referral_ui_name}"
    cookie_based_affinity = "Disabled"
    port                  = 80
    protocol              = "Http"
    request_timeout       = 30
    path                  = "/"
    connection_draining {
      drain_timeout_sec = 60
      enabled           = true
    }
    probe_name          = "${var.prefix}-${local.appgw_probe_referral_ui_name}"
    pick_host_name_from_backend_address = true
  }

  gateway_ip_configuration {
    name      = "appGatewayIpConfig"
    subnet_id = azurerm_subnet.applicationgateway.id
  }

  frontend_port {
    name = "port_80"
    port = 80
  }

  frontend_port {
    name = "port_443"
    port = 443
  }

  frontend_ip_configuration {
    name                 = "appGwPublicFrontendIp"
    public_ip_address_id = data.azurerm_public_ip.referral_ui_public_ip.id
  }

  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_http_referral_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_80"
    protocol                       = "Http"
  }
  
  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_https_referral_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_443"
    protocol                       = "Https"
    ssl_certificate_name           = "${var.prefix}-${local.appgw_ssl_cert_referral_ui_name}"
    custom_error_configuration {
        status_code           = "HttpStatus403"
        custom_error_page_url = "${azurerm_storage_account.storage_appgw_errorpage.primary_blob_endpoint}${azurerm_storage_container.container_appgw_referral_ui.name}/error403.html"
    }
    custom_error_configuration {
        status_code           = "HttpStatus502"
        custom_error_page_url =  "${azurerm_storage_account.storage_appgw_errorpage.primary_blob_endpoint}${azurerm_storage_container.container_appgw_referral_ui.name}/error502.html"
    }  
  }

  redirect_configuration {
    name                 = "${var.prefix}-${local.appgw_redirect_referral_ui_name}"
    redirect_type        = "Permanent"
    include_path         = true
    include_query_string = true
    target_listener_name = "${var.prefix}-${local.appgw_listener_https_referral_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-https-referral-ui"
    backend_address_pool_name  = "${var.prefix}-${local.appgw_bep_referral_ui_name}"
    backend_http_settings_name = "${var.prefix}-${local.appgw_backend_referral_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_https_referral_ui_name}"
    priority                   = 1
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_referral_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-http-referral-ui"
    redirect_configuration_name = "${var.prefix}-${local.appgw_redirect_referral_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_http_referral_ui_name}"
    priority                   = 2
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_referral_ui_name}"
  }

  sku {
    name     = "WAF_v2"
    tier     = "WAF_v2"
  }
    waf_configuration {
    enabled          = true
    firewall_mode    = "Detection"
    rule_set_version = "3.2"
  }
  enable_http2       = true
  
  lifecycle {
    ignore_changes = [request_routing_rule]
  }
}

resource "azurerm_application_gateway" "sd_admin_ui_app_gateway" {
  name                = "${var.prefix}-fh-appgw-sd-admin-ui"
  resource_group_name = local.resource_group_name
  location            = var.location
  tags = local.tags

  autoscale_configuration {
    min_capacity                = 1
    max_capacity                = 1
  }

  ssl_certificate {
    name     = "${var.prefix}-${local.appgw_ssl_cert_sd_admin_ui_name}"
    data     = var.ssl_cert_path_sd_admin_ui
    password = var.certificate_password
  }

  probe {
    name                        = "${var.prefix}-${local.appgw_probe_sd_admin_ui_name}"
    protocol                    = "Http"
    interval                    = "30"
    timeout                     = "30"
    unhealthy_threshold         = "3"
    pick_host_name_from_backend_http_settings = true
    path                        = "/api/health"
    match {
      status_code               = [ "200-399" ]
    }
  }

  ssl_policy {
    policy_type           = "Predefined"
    policy_name           = "AppGwSslPolicy20220101"
  }

  rewrite_rule_set {
     name                 = "${var.prefix}-${local.appgw_rewrites_sd_admin_ui_name}"
     rewrite_rule {
        name              = "NewRewrite"
        rule_sequence     = "100"
            
     response_header_configuration {
       header_name        = "Server"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "X-Powered-By"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "Strict-Transport-Security"
       header_value       = "max-age=31536000; includeSubDomains; preload"
      }
     request_header_configuration {
       header_name        = "X-Forwarded-Host"
       header_value       = var.manage_domain
      }
     }
   }

  backend_address_pool {
      name                = "${var.prefix}-${local.appgw_bep_sd_admin_ui_name}"
      ip_addresses        = [var.private_endpoint_ip_address.service_directory_admin_ui]
  }

  backend_http_settings {
    name                = "${var.prefix}-${local.appgw_backend_sd_admin_ui_name}"
    cookie_based_affinity = "Disabled"
    port                  = 80
    protocol              = "Http"
    request_timeout       = 1200
    path                  = "/"
    connection_draining {
      drain_timeout_sec = 60
      enabled           = true
    }
    probe_name          = "${var.prefix}-${local.appgw_probe_sd_admin_ui_name}"
    pick_host_name_from_backend_address = true
  }

  gateway_ip_configuration {
    name      = "appGatewayIpConfig"
    subnet_id = azurerm_subnet.applicationgateway.id
  }

   frontend_port {
    name = "port_80"
    port = 80
  }

  frontend_port {
    name = "port_443"
    port = 443
  }
  
  frontend_ip_configuration {
    name                 = "appGwPublicFrontendIp"
    public_ip_address_id = data.azurerm_public_ip.sd_admin_ui_public_ip.id
  }

  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_http_sd_admin_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_80"
    protocol                       = "Http"
  }

  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_https_sd_admin_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_443"
    protocol                       = "Https"
    ssl_certificate_name           = "${var.prefix}-${local.appgw_ssl_cert_sd_admin_ui_name}"
	  custom_error_configuration {
        status_code           = "HttpStatus403"
        custom_error_page_url = "${azurerm_storage_account.storage_appgw_errorpage.primary_blob_endpoint}${azurerm_storage_container.container_appgw_sd_admin_ui.name}/error403.html"
    }
    custom_error_configuration {
        status_code           = "HttpStatus502"
        custom_error_page_url =  "${azurerm_storage_account.storage_appgw_errorpage.primary_blob_endpoint}${azurerm_storage_container.container_appgw_sd_admin_ui.name}/error502.html"
    } 
  }

  redirect_configuration {
    name                 = "${var.prefix}-${local.appgw_redirect_sd_admin_ui_name}"
    redirect_type        = "Permanent"
    include_path         = true
    include_query_string = true
    target_listener_name = "${var.prefix}-${local.appgw_listener_https_sd_admin_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-https-sd-admin-ui"
    backend_address_pool_name  = "${var.prefix}-${local.appgw_bep_sd_admin_ui_name}"
    backend_http_settings_name = "${var.prefix}-${local.appgw_backend_sd_admin_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_https_sd_admin_ui_name}"
    priority                   = 1
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_sd_admin_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-http-sd-admin-ui"
    redirect_configuration_name = "${var.prefix}-${local.appgw_redirect_sd_admin_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_http_sd_admin_ui_name}"
    priority                   = 2
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_sd_admin_ui_name}"
  }

  sku {
    name     = "WAF_v2"
    tier     = "WAF_v2"
  }
    waf_configuration {
    enabled          = true
    firewall_mode    = "Detection"
    rule_set_version = "3.2"
  }
  enable_http2       = true
}

resource "azurerm_application_gateway" "sd_ui_app_gateway" {
  name                = "${var.prefix}-fh-appgw-sd-ui"
  resource_group_name = local.resource_group_name
  location            = var.location
  tags = local.tags

  autoscale_configuration {
    min_capacity                = 1
    max_capacity                = 1
  }

  ssl_certificate {
    name     = "${var.prefix}-${local.appgw_ssl_cert_sd_ui_name}"
    data     = var.ssl_cert_path_sd_ui
    password = var.certificate_password
  }

  probe {
    name                        = "${var.prefix}-${local.appgw_probe_sd_ui_name}"
    protocol                    = "Http"
    interval                    = "30"
    timeout                     = "30"
    unhealthy_threshold         = "3"
    pick_host_name_from_backend_http_settings = true
    path                                      = "/api/health"
    match {
      status_code               = [ "200-399" ]
    }
  }

  ssl_policy {
    policy_type           = "Predefined"
    policy_name           = "AppGwSslPolicy20220101"
  }

  rewrite_rule_set {
     name                 = "${var.prefix}-${local.appgw_rewrites_sd_ui_name}"
     rewrite_rule {
        name              = "NewRewrite"
        rule_sequence     = "100"
             
     response_header_configuration {
       header_name        = "Server"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "X-Powered-By"
       header_value       = ""
      }
     response_header_configuration {
       header_name        = "Strict-Transport-Security"
       header_value       = "max-age=31536000; includeSubDomains; preload"
      }
     request_header_configuration {
       header_name        = "X-Forwarded-Host"
       header_value       = var.find_domain
      }
     }
   }

  backend_address_pool {
    name                = "${var.prefix}-${local.appgw_bep_sd_ui_name}"
    ip_addresses = [var.private_endpoint_ip_address.service_directory_ui]
  }
  
  backend_http_settings {
    name                = "${var.prefix}-${local.appgw_backend_sd_ui_name}"
    cookie_based_affinity = "Disabled"
    port                  = 80
    protocol              = "Http"
    request_timeout       = 30
    path                  = "/"
    connection_draining {
      drain_timeout_sec = 60
      enabled           = true
    }
    probe_name          = "${var.prefix}-${local.appgw_probe_sd_ui_name}"
    pick_host_name_from_backend_address = true
  }

  gateway_ip_configuration {
    name      = "appGatewayIpConfig"
    subnet_id = azurerm_subnet.applicationgateway.id
  }

  frontend_port {
    name = "port_80"
    port = 80
  }

  frontend_port {
    name = "port_443"
    port = 443
  }

  frontend_ip_configuration {
    name                 = "appGwPublicFrontendIp"
    public_ip_address_id = data.azurerm_public_ip.sd_ui_public_ip.id
  }

  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_http_sd_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_80"
    protocol                       = "Http"
  }

  http_listener {
    name                           = "${var.prefix}-${local.appgw_listener_https_sd_ui_name}"
    frontend_ip_configuration_name = "appGwPublicFrontendIp"
    frontend_port_name             = "port_443"
    protocol                       = "Https"
    ssl_certificate_name           = "${var.prefix}-${local.appgw_ssl_cert_sd_ui_name}"
    custom_error_configuration {
        status_code           = "HttpStatus502"
        custom_error_page_url =  "${azurerm_storage_account.storage_appgw_errorpage.primary_blob_endpoint}${azurerm_storage_container.container_appgw_sd_ui.name}/error502.html"
    }
  }

  redirect_configuration {
    name                 = "${var.prefix}-${local.appgw_redirect_sd_ui_name}"
    redirect_type        = "Permanent"
    include_path         = true
    include_query_string = true
    target_listener_name = "${var.prefix}-${local.appgw_listener_https_sd_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-https-sd-ui"
    backend_address_pool_name  = "${var.prefix}-${local.appgw_bep_sd_ui_name}"
    backend_http_settings_name = "${var.prefix}-${local.appgw_backend_sd_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_https_sd_ui_name}"
    priority                   = 1
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_sd_ui_name}"
  }

  request_routing_rule {
    name                       = "${var.prefix}-fh-routing-http-sd-ui"
    redirect_configuration_name = "${var.prefix}-${local.appgw_redirect_sd_ui_name}"
    http_listener_name         = "${var.prefix}-${local.appgw_listener_http_sd_ui_name}"
    priority                   = 2
    rule_type                  = "Basic"
    rewrite_rule_set_name      = "${var.prefix}-${local.appgw_rewrites_sd_ui_name}"
  }

  sku {   
    name     = "WAF_v2"
    tier     = "WAF_v2"
  }

  waf_configuration {
    enabled          = true
    firewall_mode    = "Detection"
    rule_set_version = "3.2"
  }
  enable_http2       = true
}

# Public IP
data "azurerm_public_ip" "referral_ui_public_ip" {
  name                      = "${var.prefix}-fh-pip-referral-ui"
  resource_group_name       = local.resource_group_name
}

data "azurerm_public_ip" "sd_admin_ui_public_ip" {
  name                      = "${var.prefix}-fh-pip-sd-admin-ui"
  resource_group_name       = local.resource_group_name
}

data "azurerm_public_ip" "sd_ui_public_ip" {
  name                      = "${var.prefix}-fh-pip-sd-ui"
  resource_group_name       = local.resource_group_name
}

# Storage Accounts

resource "azurerm_storage_account" "storage_appgw_errorpage" {
  name                      			    = "${var.prefix}saappgwerror"
  resource_group_name       			    = local.resource_group_name
  location                  			    = var.location
  account_tier              			    = local.account_tier
  account_kind              			    = local.account_kind
  access_tier               			    = local.access_tier
  min_tls_version           			    = local.min_tls_version
  public_network_access_enabled 		    = local.public_network_access_enabled_storage
  account_replication_type  			    = local.account_replication_type
  infrastructure_encryption_enabled 	= local.infrastructure_encryption_enabled
  cross_tenant_replication_enabled          = false
  provisioner "local-exec" {
    command = "az storage account update --name ${self.name} --resource-group ${self.resource_group_name} --key-exp-days 365"
    interpreter = ["pwsh", "-Command"]
  }
  blob_properties {
    versioning_enabled     				    = local.versioning_enabled
    change_feed_enabled    				    = local.change_feed_enabled
    delete_retention_policy {
      days                 				    = local.delete_retention_policy_days
    }
    container_delete_retention_policy {
      days                 				    = local.container_delete_retention_policy_days
    }
  }
  tags = local.tags
}

resource "azurerm_storage_container" "container_appgw_referral_ui" {
  name                  				= "${var.prefix}sacontappgwrefui"
  storage_account_id  				= azurerm_storage_account.storage_appgw_errorpage.id
  container_access_type 				= "blob"
}

resource "azurerm_storage_blob" "blob_appgw_referral_ui_error502" {
  name                   				= "error502.html"
  storage_account_name   				= azurerm_storage_account.storage_appgw_errorpage.name
  storage_container_name 				= azurerm_storage_container.container_appgw_referral_ui.name
  type                   				= "Block"
  content_type           				= "text/html"
  source                 				= "${var.appgw_errorpage_path_referral_ui}/error502.html"
  content_md5                           = filemd5("${var.appgw_errorpage_path_referral_ui}/error502.html")
}

resource "azurerm_storage_blob" "blob_appgw_referral_ui_error403" {
  name                   				= "error403.html"
  storage_account_name   				= azurerm_storage_account.storage_appgw_errorpage.name
  storage_container_name 				= azurerm_storage_container.container_appgw_referral_ui.name
  type                   				= "Block"
  content_type           				= "text/html"
  source                 				= "${var.appgw_errorpage_path_referral_ui}/error403.html"
  content_md5                           = filemd5("${var.appgw_errorpage_path_referral_ui}/error403.html")
}

resource "azurerm_storage_container" "container_appgw_sd_admin_ui" {
  name                  				= "${var.prefix}sacontappgwsdadminui"
  storage_account_id  				= azurerm_storage_account.storage_appgw_errorpage.id
  container_access_type 				= "blob"
}

resource "azurerm_storage_blob" "blob_appgw_sd_admin_ui_error502" {
  name                   				= "error502.html"
  storage_account_name   				= azurerm_storage_account.storage_appgw_errorpage.name
  storage_container_name 				= azurerm_storage_container.container_appgw_sd_admin_ui.name
  type                   				= "Block"
  content_type           				= "text/html"
  source                 				= "${var.appgw_errorpage_path_sd_admin_ui}/error502.html"
  content_md5                           = filemd5("${var.appgw_errorpage_path_sd_admin_ui}/error502.html")
}

resource "azurerm_storage_blob" "blob_appgw_sd_admin_ui_error403" {
  name                   				= "error403.html"
  storage_account_name   				= azurerm_storage_account.storage_appgw_errorpage.name
  storage_container_name 				= azurerm_storage_container.container_appgw_sd_admin_ui.name
  type                   				= "Block"
  content_type           				= "text/html"
  source                 				= "${var.appgw_errorpage_path_sd_admin_ui}/error403.html"
  content_md5                           = filemd5("${var.appgw_errorpage_path_sd_admin_ui}/error403.html")
}

resource "azurerm_storage_container" "container_appgw_sd_ui" {
  name                  				= "${var.prefix}sacontappgwsdui"
  storage_account_id  				= azurerm_storage_account.storage_appgw_errorpage.id
  container_access_type 				= "blob"
}

resource "azurerm_storage_blob" "blob_appgw_sd_ui_error502" {
  name                   				= "error502.html"
  storage_account_name   				= azurerm_storage_account.storage_appgw_errorpage.name
  storage_container_name 				= azurerm_storage_container.container_appgw_sd_ui.name
  type                   				= "Block"
  content_type           				= "text/html"
  source                 				= "${var.appgw_errorpage_path_sd_ui}/error502.html"
  content_md5                           = filemd5("${var.appgw_errorpage_path_sd_ui}/error502.html")
}

# Create Virtual Network
resource "azurerm_virtual_network" "vnet" {
  name                = "${var.prefix}-fh-vn-01"
  address_space       = var.vnet_address_space
  resource_group_name = local.resource_group_name
  location            = var.location
  tags = local.tags
}

# Create Appgtway Subnet
resource "azurerm_subnet" "applicationgateway" {
  name                 = "${var.prefix}-fh-appgw-sn"
  address_prefixes         = var.ag_address_space
  resource_group_name = local.resource_group_name
  virtual_network_name =  azurerm_virtual_network.vnet.name
  private_endpoint_network_policies = "Disabled"
}

# Create vNET Integration Subnet
resource "azurerm_subnet" "vnetint" {
  name                 = "${var.prefix}-fh-vnetint-sn"
  address_prefixes         = var.vnetint_address_space
  resource_group_name = local.resource_group_name
  virtual_network_name =  azurerm_virtual_network.vnet.name
  private_endpoint_network_policies = "Disabled"
  delegation {
    name = "delegation"

    service_delegation {
      name    = "Microsoft.Web/serverFarms"
      actions = ["Microsoft.Network/virtualNetworks/subnets/join/action", "Microsoft.Network/virtualNetworks/subnets/prepareNetworkPolicies/action"]
    }
  }
  lifecycle {
    ignore_changes = [delegation]
  }
}
