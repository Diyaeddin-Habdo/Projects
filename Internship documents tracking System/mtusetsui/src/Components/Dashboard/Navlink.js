import {faFileExcel } from "@fortawesome/free-solid-svg-icons";
import {faFileSignature} from "@fortawesome/free-solid-svg-icons";
import {faHourglassHalf} from "@fortawesome/free-solid-svg-icons";
import {faFile} from "@fortawesome/free-solid-svg-icons";
import {faMessage} from "@fortawesome/free-solid-svg-icons";
import {faTimeline} from "@fortawesome/free-solid-svg-icons";

// Returns the link items based on the role
export const links = [
    // for Advisor
    {
        name: "Beklemede",
        path: "Pending",
        icon: faHourglassHalf,
        role: "3953,9763",
    },
    {
        name: "Onaylanan", 
        path: "Approved",
        icon: faFileSignature, 
        role: "3953,9763",
    },
    {
        name: "Onaylanmayan",
        path: "Unapproved",
        icon: faFileExcel,
        role: "3953,9763",
    },
    {
        name: "Mesajlar",
        path: "sent/Messages",
        icon: faMessage,
        role: "3953,9763",
    },
    {
        name: "Geçmiş",
        path: "History",
        icon: faTimeline,
        role: "3953,9763",
    },
    // for students
    {
        name: "Yüklenecekler",
        path: "Documents",
        icon: faFile,
        role: "1753",
    },
    {
        name: "Mesajlar",
        path: "received/Messages",
        icon: faMessage,
        role: "1753",
    },
    {
        name: "Geçmiş",
        path: "History",
        icon: faTimeline,
        role: "1753",
    }
] 