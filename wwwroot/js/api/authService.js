const ACCESS_TOKEN_KEY = 'accessToken';
const REFRESH_TOKEN_KEY = 'refreshToken';

export function getAccessToken() {
    return sessionStorage.getItem(ACCESS_TOKEN_KEY);
}

export function setTokens({ accessToken, refreshToken }) {
    sessionStorage.setItem(ACCESS_TOKEN_KEY, accessToken);
    sessionStorage.setItem(REFRESH_TOKEN_KEY, refreshToken);
}

export function clearTokens() {
    sessionStorage.removeItem(ACCESS_TOKEN_KEY);
    sessionStorage.removeItem(REFRESH_TOKEN_KEY);
}

export async function refreshToken() {
    const refresh = sessionStorage.getItem(REFRESH_TOKEN_KEY);
    if (!refresh) return null;
    
    const res = await fetch('/auth/refresh', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ refreshToken: refresh }),
    });
    if (res.ok) {
        const data = await res.json();
        setTokens(data);
        return data.accessToken;
    } else {
        clearTokens();
        return null;
    }
}