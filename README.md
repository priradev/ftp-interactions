# FTP Interact
This .NET-based GitHub Action top copy or delete files

## Inputs
| Parameter                 | Required  | Default                  | Description                                                                               |
| :---                      | :---      | :---                     | :---                                                                                      |
| **server**                | **Yes**   |                          | Address for the destination server.                                                       |
| port                      | No        | **21**                   | Port for the destination server.                                                          |
| **username**              | **Yes**   |                          | Username for the destination server.                                                      |
| **password**              | **Yes**   |                          | Password for the destination server.                                                      |
| localDir                  | No        | **/**                    | Local Directory from which to upload.                                                     |
| deleteFileAppOfflineHtm   | No        | **false**                | Delete app_offline.htm from server.                                                       |

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
    - name: Get files
      uses: actions/checkout@v2.3.4
      
    - name: FTP Interact
      uses: cinderblockgames/ftp-action@v1.2.2
      with:
        # required
        server: ftp.example.com
        username: example@example.com
        password: ${{ secrets.FTP_PASSWORD }}
        # optional
        port: 22
        source: src/path
        destination: target/path
        skipUnchanged: true
        skipDirectories: .github|.well-known|configs|private-keys
        test: true
```
