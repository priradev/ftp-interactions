# FTP Interact
This .NET-based GitHub Action to copy or delete files

Code is based on https://github.com/cinderblockgames/ftp-action

## Inputs
| Parameter               | Required | Default   | Description                          |
| :---------------------- | :------- | :-------- | :----------------------------------- |
| **host**                | **Yes**  |           | Address for the destination host     |
| port                    | No       | **21**    | Port for the destination host        |
| **username**            | **Yes**  |           | Username for the destination host    |
| **password**            | **Yes**  |           | Password for the destination host    |
| localDir                | No       |           | Local Directory from which to upload |
| copyLocalDir            | No       | **false** | Copy files from localDir             |
| deleteFileAppOfflineHtm | No       | **false** | Delete app_offline.htm from host     |
| ignoreCertificateErrors | No       | **false** | Ignore certificate errors            |

## Example Workflow
```
# Workflow name
name: Deploy site to live
 
on:
  # Run automatically on push to main branch
  push:
    branches: [ main ]
    paths:
    - 'src/**'
  # Allow manual trigger
  workflow_dispatch:

jobs:
  web-deploy:
    name: Deploy
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2.3.4
      
    - name: FTP Interact
      uses: priradev/ftp-interactions@v0.2.5-beta
      with:
        # required
        host: ${{ secrets.FTP_SERVER }}
        username: example@example.com
        password: ${{ secrets.FTP_PASSWORD }}
        # one of the next is required
        copyLocalDir: true
        deleteFileAppOfflineHtm: true
        # optional
        port: 21
        localDir: ./Resources/Offline/
        ignoreCertificateErrors: false
```
