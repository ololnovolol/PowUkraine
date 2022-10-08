import React from 'react';
import MessageForm from './form';
import styled from 'styled-components';
import '../../style/forms/form.css';

const CenterBlock = styled.div`
    width: 50%;
    height: auto;
    background: #fff;
    position: absolute;
    align-items: center;
    margin-top: 5rem;
    margin-bottom: 5rem;
    left: 25%;
    padding: 25px 25px;
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
`;

function Message() {
    return (
        <>
            <CenterBlock>
                <div className="hh">
                    <h1>Create important message</h1>
                </div>

                <MessageForm />
            </CenterBlock>
        </>
    );
}

export default Message;
