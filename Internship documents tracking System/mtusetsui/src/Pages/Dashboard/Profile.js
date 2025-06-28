import React, { useEffect, useState } from 'react';
import { Card, Form, Button, Container, Row, Col } from 'react-bootstrap';
import axios from 'axios';
import { baseURL, isTokenExpired, logout, STUDENT, USER } from '../../API/Api';
import Cookie from 'cookie-universal';
import { Axios } from '../../API/axios';

const Profile = () => {
  const [userData, setUserData] = useState({
    name: '',
    email: '',
    password: '',
    phone: '',
    image: '',
    role: '',
  });


  const cookie = Cookie();

  useEffect(() => {
    
    const fetchUserData = async () => {
    
      const id = Number(cookie.get('id'));
      const role = cookie.get("role");
      const user = String(role) === "1753" ? STUDENT : USER;
      if(!isTokenExpired())
      {
        try {
          const response = await axios.get(`${baseURL}/${user}/${id}`, {
              headers: {
                  Authorization: "Bearer " + cookie.get('token'),
              }
          }).then((data) => setUserData(data.data));
  
        } catch (error) {
          console.error('Error fetching user data:', error);
        }
      }
      else
        logout();
    };

    fetchUserData();
  }, []);

  return (
    <Container className="mt-5">
      <Row className="justify-content-center">
        <Col md={8}>
          <Card className="shadow-sm p-3 mb-5 bg-white rounded">
            <Card.Body>
              <div className="d-flex justify-content-center mb-4">
                <img
                  src={userData.imagePath || 'default-profile.png'}
                  alt="Profile"
                  className="rounded-circle"
                  width="150"
                  height="150"
                />
              </div>
              <Card.Title className="text-center mb-4">Profile Information</Card.Title>
              <Form>
                <Form.Group as={Row} className="mb-3">
                  <Form.Label column sm={3}>Name:</Form.Label>
                  <Col sm={9}>
                    <Form.Control type="text" value={userData.name} readOnly />
                  </Col>
                </Form.Group>

                <Form.Group as={Row} className="mb-3">
                  <Form.Label column sm={3}>Email:</Form.Label>
                  <Col sm={9}>
                    <Form.Control type="email" value={userData.email} readOnly />
                  </Col>
                </Form.Group>

                <Form.Group as={Row} className="mb-3">
                  <Form.Label column sm={3}>Phone:</Form.Label>
                  <Col sm={9}>
                    <Form.Control type="tel" value={userData.phone} readOnly />
                  </Col>
                </Form.Group>

                <Form.Group as={Row} className="mb-3">
                  <Form.Label column sm={3}>Role:</Form.Label>
                  <Col sm={9}>
                    <Form.Control type="text" value={userData.role === "3953" ? "Admin" : (userData.role === "9763" ? "Teacher" : "Student")} readOnly />
                  </Col>
                </Form.Group>

              </Form>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default Profile;
