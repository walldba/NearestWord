<p align="center" >
    <a href="https://www.flaticon.com/free-icon/bird_212800#"> 
        <img alt="logo" src="https://raw.githubusercontent.com/walldba/NearestWord/master/nearLogo.png" width="210" height="210">
    </a>
</p>

# NearestWord

Simple .NET and .NET Core package to find a closer word in a list of synonyms.

[![][build-img]][build]
[![][nuget-img]][nuget]

[build]:     https://ci.appveyor.com/project/walldba/nearestword
[build-img]: https://ci.appveyor.com/api/projects/status/qov7081vpw3ex354?svg=true

[nuget]:     https://www.nuget.org/packages/nearestword
[nuget-img]: https://badge.fury.io/nu/NearestWord.svg

This package clears a string and compares it to a list of synonyms accepted a limited of changes to find the nearest word into the list. 
"operations" means the insertion, exclusion or replacement of a character.

#### Example

| Typed Word    | Example word in list  | Operations  |
| ------------- |:-------------:| -----:|
| Brazil        | Brasil        |   1   |
| My income tax      |    Income tax |2
| Intention     | Execution     |   4   |
| Today is Friday!! | Today is Monday      |    5 |


## Usage
Do this to remove the irrelevant word, remove the special characters, and use Levenshtein to get the nearest word in the synonym list.
```cs
var typedWord = "Intention";
var foundWord = ReplaceRegex(typedWord, RemoveStopWord(regexPattern))
                                 .RemoveSpecialCharacters()
                                 .LevenshteinDistance(synonyms, maxChanges);
```
> if you prefer, you can use the extension methods separately just to clean your string

## Provided extension methods
* .ReplaceRegex()
* .CreateRegexPattern()
* .RemoveSpecialCharacters()
* .LevenshteinDistance()
