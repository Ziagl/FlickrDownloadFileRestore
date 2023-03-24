# FlickrDownloadFileRestore
Script to restore filename and datetime of Images downloaded from Flickr

## How to use
* create a new folder
* move FlickrRestore.exe to that folder
* create 2 subfolders "source" and "target"
* unzip all *.jpg and *.json files from flickr to "source" folder
* run "FlickrRestore.exe source target" in command line
* finish

## What is does
This script copies all of your images from source folder to target. It does not delete images!
If it findes a suitable metadata file (*.json) for an image filename, it changes the filename
to the creation date from that file and also sets correct creation date, so you can order your
images in the correct order.

After the script is finished, you can delete source folder. Target folder contains all images. All
images that are restored with metadata in "restored" subfolder, all other images in "copied" subfolder.

## Detailed description
You can find a detailed description in German with screenshots on my blog:
https://developer-blog.net/wp-admin/post.php?post=12282&action=edit