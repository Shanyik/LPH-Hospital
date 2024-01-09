import { useState } from "react";


const SearchField = ({ searchButton, setSearchValue}) => {

    const [seacrchVal, setSearchVal] = useState("")
    const [SearchRes, setSearchRes] = useState(null)

    const checkSearchValue = () => {
        const trimmedSearchValue = seacrchVal.trim();
        if(trimmedSearchValue.length === 11){
            setSearchRes(true);
            setSearchValue(trimmedSearchValue);
        }else{
            setSearchRes(false);
        }
    }

    const isInputValid = (e) => {

        const input = e.target.value;
        const pattern = /^[0-9]+$/;
        const removedValue = removeWhiteSpaces(input);

        if(pattern.test(removedValue)){
            let newValue = addWhiteSpaces(removedValue);
            e.target.value = newValue;
            setSearchRes(null);
        }else{
            const removedValue =  input.replace(/[^0-9]/g, '');
            e.target.value = addWhiteSpaces(removedValue);
            setSearchRes(false)
        }

    }

    const removeWhiteSpaces = (input) => {
        const indexes = [3, 7];
        let newString = "";
        for (let index = 0; index < input.length; index++) {
            if (!indexes.includes(index) || input[index] !== "-") {
                newString += input[index];
            }

        }

        return newString;
    }

    const addWhiteSpaces = (input) => {
        const indexes = [3, 6];
        console.log(input)
        let newString = "";
        for (let index = 0; index < input.length; index++) {
            if (indexes.includes(index)) {
                newString += "-" + input[index];
            } else {
                newString += input[index];
            }

        }

        return newString;
    }


    return (
        <div id="searchField">
            <div id="displayError">
                {
                SearchRes === false ? <>Invalid Input</> :
                SearchRes === true ? <>Valid Medical number</> :
                <></>
                }
            </div>
            <input type="text" placeholder="Seacrh Patient"
            maxLength={11}
            onBlur={(e)=> checkSearchValue(e.target.value)}
            onChange={(e) => { isInputValid(e)
            setSearchVal(e.target.value) }}/>
            <button onClick={() => {searchButton(); checkSearchValue()}}>Search</button>
        </div>
    )
}

export default SearchField;