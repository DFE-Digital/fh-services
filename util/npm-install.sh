#!/bin/bash

unset stop

_term() {
    stop=1
}

trap _term INT

Folders=(
    src/ui/connect-dashboard-ui/src/FamilyHubs.RequestForSupport.Web
    src/ui/connect-ui/src/FamilyHubs.Referral.Web
    src/ui/find-ui/src/FamilyHubs.ServiceDirectory.Web
    src/ui/idam-maintenance-ui/src/FamilyHubs.Idams.Maintenance.UI
    src/ui/manage-ui/src/FamilyHubs.ServiceDirectory.Admin.Web
)

for Folder in ${Folders[*]}
do
    echo "Running 'npm install' for $Folder"
    cd "$(dirname "$0")/../$Folder"

    retVal=$?
    if [ $retVal -ne 1 ]; then
        npm install || true
        cd - > /dev/null
    fi

    if [ "${stop}" ]; then
        exit 0
    fi
done
