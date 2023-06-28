# Gigabyte Monitor Controller

## Introduction

API to change Gigabyte's monitors KVMs without OSD Sidekick or other offical programs installed.

 - M34WQ - https://www.gigabyte.com/Monitor/M34WQ
 - P27Q-P - https://www.aorus.com/pl-pl/monitors/M27Q-P

## How to use

Connect a monitor with USB C or USB B cable. Run a Switcher program to simply switch KVMs or call ToggleKvm method in your custom implementation.

## Limitations

- The solution should be cross-platform (.NET Standard) but I tested it only on Windows 10. 
- Multiple monitors - it works with M34WQ and P27Q-P at the same time but it won't support two or more monitors of the same type. It should be pretty easy to implement it though - just iterate over the found devices.
- You can call it only from the computer connected to currently selected KVM option.

## Credits

While searching for existing solutions I found a few projects. Big thanks for the work done to:
- @kelvie at https://github.com/kelvie/gbmonctl
- @P403n1x87 at https://github.com/P403n1x87/m27q


## Support

I do not plan on further development.
