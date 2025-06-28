import Cookie from "cookie-universal";

export const baseURL = `https://localhost:7114/api`;
export const acdLOGIN = "Teachers/Auth/Login";
export const stdLOGIN = "Students/Auth/Login";
export const USERS = "Teachers/All";
export const USER = "Teachers";
export const STUDENT = "Students";
export const DOCUMENTS = "Documents";
export const sDOCUMENTS = "Documents/Student";
export const sPENDING = "Students/Pending";
export const sTEACHERAPPROVAL = "Students/TeacherApproval";
export const sADMINAPPROVAL = "Students/AdvisorApproval";
export const sTEACHERUNAPPROVAL = "Students/TeacherUnapproval";
export const sADMINUNAPPROVAL = "Students/AdvisorUnapproval";
export const STATUS = "Documents/Status";
export const MESSAGES = "Messages";
export const SendMESSAGES = "Messages/From";
export const TEACHER = "Teachers/Name";
export const STUDENTName = "Students/Name";
export const LOGS = "Logs";
export const tLOGS = "Logs/student";
export const sIMAGE = "students/image";
export const tIMAGE = "teachers/image";
export const GOOGLE_CALL_BACK = "";









export const isTokenExpired = () => {
    const cookie = Cookie();
    const token = cookie.get("token");
    if (!token) 
        return true;
    const payload = JSON.parse(atob(token.split('.')[1]));
    const now = Math.floor(Date.now() / 1000);
    return now >= payload.exp;
};

export const logout = () => {
    const cookie = Cookie();
    cookie.removeAll();
    window.location.pathname = '/';
};