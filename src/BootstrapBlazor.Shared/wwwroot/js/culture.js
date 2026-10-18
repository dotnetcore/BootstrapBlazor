window.bbCulture = {
    get: () => document.documentElement.lang,
    set: culture => {
        document.cookie = `.AspNetCore.Culture=${encodeURIComponent(`c=${culture}|uic=${culture}`)};path=/;max-age=31536000;samesite=lax`;
    }
};
