import React from 'react';
import MessageForm from './form';
import styled from 'styled-components';

const CenterBlock = styled.div`
    background: #3a5431 none repeat scroll 0% 0%;
    width: 50%;
    background: #fff;
    height: 10px;
    position: absolute;
    align-items: center;
    left: 25%;
    top: 25%;
    min-width: 0px;
    min-height: 0px;
    justify-content: center;
    padding: 25px 25px;
    z-index: 0;
    color: black;
`;

function Message() {
    return (
        <>
            <div>
                <CenterBlock>
                    <MessageForm />
                </CenterBlock>
            </div>
        </>
    );
}

export default Message;
