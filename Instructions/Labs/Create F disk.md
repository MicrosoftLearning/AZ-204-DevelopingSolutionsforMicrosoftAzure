### In order to create a VHD on Windows 10, do the following:

1. Open Start.
1. Search for Disk Management and click the top result to launch the experience.
1. Click the Action button.
1. Click the Create VHD option.
1. Click the Browse button and locate the folder you want to store the virtual disk.
1. In the "File name" field enter a name for the drive.
1. Use the "Save as type file" drop-down menu and select Virtual Disk files (*.vhdx) . Or select Virtual Disk files (*.vhd) if you're planning to create a VHD file.
1. Click the Save button.
1. Under "Virtual hard disk size," specify the size of the drive in megabytes (MB), gigabytes (GB), or terabytes (TB).
1. Under "Virtual hard disk format," select the VHDX option. (While using a VHDX format is recommended, you can also select the VHD format, but use it only if required.)
1. Under "Virtual hard disk type," select the Dynamic expanding option. (If you have selected the VHD format in the previous step, it's recommended to select the Fixed size option when selecting a type.)
1. Click OK.
1. Once you complete these steps, you'll have created a VHD that you can then set up and use with any compatible version of Windows.

### How to set up a VHDX or VHD on Windows 10
Using the above steps, you created a VHD, but it's empty without any data or file system. To make it useful, you need to initialize the disk, create a partition, and format the drive using these steps:

1. Right-click the newly created drive button on the far-left side, and click the Initialize Disk option.
1. Select the disk from the list.
1. Check the MBR (Master Boot Record) option. (You could also select the "GPT (GUID Partition Table)" option, but this option isn't supported by all versions of Windows.)
1. Click OK.
1. Right-click the Unallocated space, and select the New Simple Volume option.
1. Click Next.
1. Specify the size of the partition. (Leave this option unchanged if you're planning to use all the available space for the partition.)
1. Click Next.
1. Using the drop-down menu, select the drive letter you want to assign to the drive.
1. Click Next.
1. Under the Format this volume with the following settings section, make sure to use the following options:
    File System - NTFS.
    Allocation unit size - Default.
    Volume label - Use the name of the drive file, but you can enter any name.
    Perform a quick format - Formats the drive faster.
    Enable file and folder compression - If it's not required, don't select it (optional).
1. Click Next.
1. Click Finish.
After completing these steps, the VHDX or VHD will be initialized, partitioned, and formatted. The VHD will mount automatically, and you can now access and save files using File Explorer.