var TXSpell = (function () {
    "use strict";

    async function check(text, language = "en_US") {
        var serviceURL = "api/spell/check?text=" + text + "&language=" + language;
        return await callEndpoint(serviceURL);
    }

    async function createSuggestions(incorrectWord, language = "en_US", max = 10) {
        var serviceURL = "api/spell/createsuggestions?incorrectWord=" + incorrectWord + "&language=" + language + "&max=" + max;
        return await callEndpoint(serviceURL);
    }

    async function createSynonyms(text, language = "en_US") {
        var serviceURL = "api/spell/createsynonyms?text=" + text + "&language=" + language;
        return await callEndpoint(serviceURL);
    }

    async function detectLanguageScopes(text) {
        var serviceURL = "api/spell/detectlanguagescopes?text=" + text;
        return await callEndpoint(serviceURL);
    }

    async function callEndpoint(serviceURL) {
        return new Promise(resolve => {
            $.ajax({
                type: "GET",
                url: serviceURL,
                contentType: 'application/json',
                success: successFunc,
                error: errorFunc
            });

            function successFunc(data, status) {
                resolve(data);
            }

            function errorFunc(data) {
                throw "Request failed. Please try again later.";
            }
        });
    }

    return {
        createSuggestions: createSuggestions,
        check: check,
        createSynonyms: createSynonyms,
        detectLanguageScopes: detectLanguageScopes
    }

}(TXSpell));