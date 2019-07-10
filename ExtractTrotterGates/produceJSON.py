import argparse
import json

# add command line arguments to run the python script
parser = argparse.ArgumentParser(
    description='Create JSON file from C# output.')

parser.add_argument("filepath", help="path to .NET output", type=str)
parser.add_argument("-o", "--outpath", help="output name", type=str)
args = parser.parse_args()
path = args.filepath

outpath = "./hamiltonian.json"

data = {}
terms = []
constants = {}

if args.outpath:
    assert args.outpath[-5:] == ".json", "Must use a .json filename"
    assert len(args.outpath) > 5, "Must have a named file"
    outpath = args.outpath

print(f"Outputting file to {outpath}")

# parse the file
with open(path) as f: 
    # this script needs to have resiliency from extraneous lines 
    # remember, it is the .NET output, so it may begin with error lines
    hasTermInfo = False
    hasConstantInfo = False
    for line in f:
        cleanedLine = line.rstrip()
        if (cleanedLine == "----- END FILE -----"):
            print("End of file reached")
        elif (cleanedLine == "----- BEGIN ORACLE WRITE -----"):
            # next lines will have term info
            hasTermInfo = True
        elif (cleanedLine == "----- END ORACLE WRITE -----"):
            # Oracle writing process is over; we're now in metadata and constant time
            hasTermInfo = False
            hasConstantInfo = True
        elif hasTermInfo:
            # ingest the term here
            print("not implemented yet LOL")
            termData = {
                "type":"ZTerm",
                "angle":-0.48954,
                "controls":[],
                "targets":[],
                "parity":[]
            }
            terms.append(termData)
        elif hasConstantInfo:
            # take in constant information, in the format "name: data"
            print("not implemented yet either LOL")
            keyValuePair = cleanedLine.split(": ")
            print(keyValuePair)
            constants[keyValuePair[0]] = keyValuePair[1]
        elif not cleanedLine:
            print("hello??")
        # else:
        #     print("test")

        # print(line)

data = {
    "constants":constants,
    "terms":terms
}


# output the data 
with open(outpath, 'w') as outfile:
    json.dump(data, outfile)
