import { createContext, useContext, useState,useEffect } from 'react';

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
