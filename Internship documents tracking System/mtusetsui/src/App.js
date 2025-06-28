import HomePage from './Pages/Website/HomePage.js';
import Login from './Pages/Auth/AuthOperation/Login.js';
import RequireAuth from './Pages/Auth/Protecting/RequireAuth.js';
import Err404 from './Pages/Auth/Errors/404.js';
import RequireBack from './Pages/Auth/Protecting/RequireBack.js';
import { Route,Routes } from 'react-router-dom';
import './App.css';
import Dashboard from './Pages/Dashboard/Dashboard.js';
import Website from './Pages/Website/Website.js';
import Profile from './Pages/Dashboard/Profile.js';
import Documents from './Pages/Dashboard/Student Documents/Documents.js';
import PendingStudents from './Pages/Dashboard/Pending/PendingStudents.js';
import StudentDocuments from './Pages/Dashboard/Pending/StudentDocuments.js';
import RecievedMessages from './Pages/Dashboard/Student Documents/RecievedMessages.js';
import ApprovedStudents from './Pages/Dashboard/Approved/ApprovedStudents.js';
import ApprovedDocuments from './Pages/Dashboard/Approved/ApprovedDocument.js';
import UnapprovedStudents from './Pages/Dashboard/Unapproved/UnapprovedStudents.js';
import UnapprovedDocuments from './Pages/Dashboard/Unapproved/UnapprovedDocument.js';
import Messages from './Pages/Dashboard/Messages/Messages.js';
import History from './Pages/Dashboard/History/history.js';


export default function App() {
   
    return (
        <div className="App">
            <Routes>

                {/* publich routes*/}
                <Route element={<Website/>}>
                    <Route path="/" element={<HomePage />} />
                </Route>
                <Route path="/" element={<RequireBack/> } >
                    <Route path="/std-login" element={<Login title = "std" />} ></Route>
                    <Route path="/acd-login" element={<Login title = "acd" />} ></Route>
                </Route>
                <Route path="/*" element={<Err404/> } />



                {/*Protected routes*/}
                {/*Advisor,teacher,Student*/}
                <Route element={<RequireAuth allowedRole={["3953","9763","1753"] } />}>
                    <Route path="/dashboard" element={<Dashboard />} >
                    <Route path="profile" element={<Profile />} />
                    <Route path="History" element={<History />} />

                        {/* Advisor,Teacher */}
                        <Route element={<RequireAuth allowedRole={['3953','9763']} />}>
                            <Route path="pending" element={<PendingStudents />} />
                            <Route path="pending/:id" element={<StudentDocuments />} />
                            <Route path="Approved" element={<ApprovedStudents />} />
                            <Route path="Approved/:id" element={<ApprovedDocuments />} />
                            <Route path="Unapproved" element={<UnapprovedStudents />} />
                            <Route path="Unapproved/:id" element={<UnapprovedDocuments />} />
                            <Route path="sent/Messages" element={<Messages />} />
                        </Route>

                        {/* Student */}
                        <Route element={<RequireAuth allowedRole={['1753']}/>}>
                            <Route path="Documents" element={<Documents/> }/>
                            <Route path="received/Messages" element={<RecievedMessages/> }/>
                        </Route>
                        
                    </Route>    
                </Route>
            </Routes>
        </div>
    );
};
