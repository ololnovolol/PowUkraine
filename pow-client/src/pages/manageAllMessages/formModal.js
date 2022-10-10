import React, { useState } from 'react';
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

const BgButton = styled.button`
    background: #88a87d none repeat scroll 0% 0%;
    height: 40px;
    weight: 100px;
    border-color: #fff;
    color: #f0a30a;
    border-radius: 5px;
    font-size: 16px;
    margin-top: 1rem;
    margin-left: 42%;
    &:hover {
        color: #fff;
        background: #6d8764;
        cursor: pointer;
    }
`;

const Block = styled.div`
    font-size: 1rem;
    position: relative;
    top: 2rem;
    border: 1 rem solid;
`;

const Form = styled.form`
  border-radius: 5px;
  }
`;

function FormModal(params) {
    const [msgData, setMsgData] = useState({
        Title: params.array.title,
        Id: params.array.id,
        EventDate: params.array.eventDate,
        UserPhone: params.array.phone,
        PhoneNumber: params.array.phoneNumber,
        Description: params.array.description,
        CreatedDate: params.array.createdDate,
        Marked: params.array.marked,
        Files: params.array.files,
    });

    function handle(e) {
        const newData = { ...msgData };
        newData[e.target.id] = e.target.value;
        setMsgData(newData);
        console.log(newData);
    }

    return (
        <div>
            <Form>
                <div className="hh">
                    <h1>Details Message</h1>
                </div>
                <Block>
                    <label>
                        ID
                        <input
                            id="id"
                            name="id"
                            value={msgData.Id}
                            readonly
                        />{' '}
                    </label>
                    <label>
                        Title
                        <input
                            id="title"
                            name="title"
                            value={msgData.Title}
                            readonly
                        />{' '}
                    </label>
                    <label>
                        Event Date
                        <input
                            id="eventDate"
                            name="eventDate"
                            value={msgData.EventDate}
                            readonly
                        />
                    </label>
                    <label>
                        User Phone
                        <input
                            id="phone"
                            name="phone"
                            value={msgData.UserPhone}
                            readonly
                        />
                    </label>
                    <label>
                        Phone Number
                        <input
                            id="phoneNumber"
                            name="phoneNumber"
                            value={msgData.PhoneNumber}
                            readonly
                        />
                    </label>
                    <label>
                        Marked
                        <input
                            id="marked"
                            name="marked"
                            value={msgData.Marked}
                            readonly
                        />
                    </label>
                    <label>
                        Description
                        <textarea
                            id="eventDate"
                            name="eventDate"
                            type={'textarea'}
                            value={msgData.Description}
                            readonly
                        />
                    </label>
                </Block>
            </Form>
        </div>
    );
}

export default FormModal;
