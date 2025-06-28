import { createContext, useState,useEffect } from 'react';

// handle window size
export const WS = createContext(null);
export default function WindowContext({ children }) {

    const [WindowSize, SetWindowSize] = useState(window.innerWidth);

    useEffect(() => {
        function SetWindowWidth() {
            SetWindowSize(window.innerWidth);
        }

        window.addEventListener("resize", SetWindowWidth);

        // clean up
        return () => {
            window.removeEventListener("resize", SetWindowWidth);
        };

    }, [])

    return (
        <WS.Provider value={{ WindowSize, SetWindowSize }}> {children} </WS.Provider>
    );
}
