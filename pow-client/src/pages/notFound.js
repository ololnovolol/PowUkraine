import React from "react";
import { Link } from "react-router-dom";
import { Col, Image } from "react-bootstrap";

export default function NoFound(){
    return(
        <div className="notFound">        
            <div className="heroContent d-flex align-items-center">
                <Col xs={12}>
                    <Image className="image404" src="https://bitsofco.de/content/images/2018/12/broken-1.png" />
                    <div className="noResultMessage">
                        <h2 className="mb-2">Oh-oh! Page not Found</h2>
                        <p>Please go back to <Link to="/">Home Page</Link>.</p>
                    </div>
                </Col>
            </div>
        </div>
    );
}