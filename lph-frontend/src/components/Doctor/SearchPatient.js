import { useState } from "react";


const SearchField = ({ searchButton, setSearchValue, searchFetchValue }) => {

    const [seacrchVal, setSearchVal] = useState("")
    const [errorState, setErrorState] = useState("")

    const chechkSearchValue = () => {
        const trimmedSearchValue = seacrchVal.trim();
        if(trimmedSearchValue === ""){
            setErrorState("Invalid input")
        }else{
            setErrorState("")
        }
    }

    return (
        <div id="searchField">
            <div id="displayError">
                {
                errorState === "Invalid input" ? <>Invalid Input</> : <></>
                }
            </div>
            <input type="text" placeholder="Seacrh Patient" onChange={(e) => { setSearchValue(e.target.value); setSearchVal(e.target.value) }}></input>
            <button onClick={() => {searchButton(); chechkSearchValue()}}>Search</button>
        </div>
    )
}

export default SearchField;