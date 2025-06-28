import { createContext, useState } from 'react';

// handle menu status (open or close)
export const Menu = createContext(true);
export default function MenuContext({ children }) {

    const [IsOpen, SetIsOpen] = useState(true);
    return (
        <Menu.Provider value={{ IsOpen, SetIsOpen }} >{children}</Menu.Provider>
    );
}