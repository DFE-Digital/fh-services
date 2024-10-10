prefix = "s181p02"
environment = "Prod"

vnet_address_space = ["10.0.4.0/24"] # Range is 10.0.4.0 - 10.0.3.255
ag_address_space = ["10.0.4.0/26"] # Range is 10.0.4.0 - 10.0.4.63
vnetint_address_space = ["10.0.4.80/28"] # Range is 10.0.4.80 - 10.0.4.95
pvtendpt_address_space = ["10.0.4.96/28"] # Range is 10.0.4.96 - 10.0.4.111
sql_server_address_space = ["10.0.4.112/28"] # Range is 10.0.4.112 - 10.0.4.127

appgw_errorpage_path_referral_ui = "modules/fhinfrastructurestack/errorpages/referral_ui/preprod"
appgw_errorpage_path_sd_admin_ui = "modules/fhinfrastructurestack/errorpages/sd_admin_ui/preprod"
appgw_errorpage_path_sd_ui = "modules/fhinfrastructurestack/errorpages/sd_ui/preprod"

service_principals = {
  reader_usr_group_object_id = "2c713f1b-2c2a-413c-879f-3b48a121bc19"
  delivery_team_user_group_object_id = "17c0fa92-3b66-4819-a08a-212778ddf7af"
  referral_data_encryption_enterprise_object_id = "423b9e4a-80f3-41f6-b31f-7cacc35267c2"
  idam_data_encryption_enterprise_object_id = "876810a2-f59e-4e1c-b683-736bf74ca0e2"
  notification_data_encryption_enterprise_object_id = "1d22c1b7-1522-4a53-8ef4-6c69e723d446"
  ado_enterprise_object_id = "3c30b876-66b4-4aa9-848b-e4657f0c6973"
  github_enterprise_object_id = "9019f685-bb2c-4700-805a-e861d045de64"
}

connect_domain = "preprod.connect-families-to-support.education.gov.uk"
manage_domain = "preprod.manage-family-support-services-and-accounts.education.gov.uk"
find_domain = "preprod.find-support-for-your-family.education.gov.uk"

private_endpoint_ip_address = {
  referral_api = "10.0.4.100"
  referral_ui = "10.0.4.101"
  service_directory_api = "10.0.4.102"
  service_directory_ui = "10.0.4.103"
  service_directory_admin_ui = "10.0.4.104"
  notification_api = "10.0.4.105"
  referral_dashboard_ui = "10.0.4.106"
  idam_api = "10.0.4.107"
  report_api_ip = "10.0.4.108"
  open_referral_mock = "10.0.4.110"
  open_referral_func = "10.0.4.111"
  sql_server = "10.0.4.116"
  idam_maintenance_ui = "10.0.4.99"
}

pvt_endp_report_stg_api_ip_address = "10.0.4.109"