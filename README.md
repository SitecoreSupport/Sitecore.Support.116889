# Sitecore.Support.116889

Cross site links use site's context language instead of site's target language.

## Main

This repository contains Sitecore Patch #116889, which extends the default `Sitecore.Links.LinkProvider` to use target site language for cross site links.

## Deployment

To apply the patch on either CM or CD server perform the following steps:

1. Place the `Sitecore.Support.116889.dll` assembly into the `\bin` directory.
2. Place the `Sitecore.Support.116889.config` file into the `\App_Config\Include\zzz` directory.

## Content 

Sitecore Patch includes the following files:

1. `\bin\Sitecore.Support.116889.dll`
2. `\App_Config\Include\zzz\Sitecore.Support.116889.config`

## License

This patch is licensed under the [Sitecore Corporation A/S License](LICENSE).

## Download

Downloads are available via [GitHub Releases](https://github.com/SitecoreSupport/Sitecore.Support.116889/releases).
