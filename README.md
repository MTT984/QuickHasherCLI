# QuickHasherCLI

QuickHasherCLI is a simple tool that generates hashes to be used in custom modding scripts for NFS World(works on other titles).

Originally this tool was created for internal use, helping in the creation of scripts for visualparts script, vinyls, commerce.bin and etc... according to demand of our server

## **How to Use**

To use this tool, you just need to provide an input string in the following format:

```quickhashercli [hashType]:[hashInput]```

*Multiple inputs are supported. it just need to be separated by a space*

The following hash types are supported for hash conversion:

    vlt
    vlt-int
    vlt-uint
    bin
    bin-int
    bin-uint
    commerce

## **Usage example**
```quickhashercli vlt-int:text1
quickhashercli vlt-int:text1 vlt:text2
quickhashercli bin:text1 bin-int:text2
quickhashercli commerce:text1
quickhashercli vlt:text1 bin:text2 commerce:text1 bin-int:text1
```

## **Credits**

The original code for hash conversion is from **NFSTools/NFS-ModTools** repository
