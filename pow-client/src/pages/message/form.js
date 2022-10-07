import React, { useState, useRef } from 'react';
import styled from 'styled-components';
import { useLocation } from 'react-router-dom';
import axios from 'axios';

const SmButton = styled.button`
    background: #88a87d none repeat scroll 0% 0%;
    height: 40px;
    border-color: #fff;
    color: #fff;
    border-radius: 5px;
    align-items: center;
    font-size: 16px;
    &:hover {
        color: #f0a30a;
        background: #6d8764;
        cursor: pointer;
    }
`;

const BgButton = styled.button`
    background: #88a87d none repeat scroll 0% 0%;
    height: 40px;
    border-color: #fff;
    weight: 100%;
    position: relative;
    margin: right 10px;
    color: #f0a30a;
    border-radius: 5px;
    justify-content: space-between;
    font-size: 16px;
    &:hover {
        color: #fff;
        background: #6d8764;
        cursor: pointer;
    }
`;

const Block = styled.div`
    font-size: 1rem;
    position: relative;
    top: 3rem;
    border: 1 rem solid;
`;

const Form = styled.form`
  border-radius: 5px;
  }
`;

export const MessageForm = props => {
    const [data, loadData] = useState({
        Title: '',
        Description: '',
        Data: '',
        PhoneNumber: '',
        Location: [],
        Attachment: [],
    });
    const [file, setFile] = useState([]);
    let loc = useLocation();
    const [location, setLocation] = useState({
        Longitude: '0',
        Latitude: '0',
        MapUrl: '',
    });

    if (loc.state !== undefined && location.Latitude === '0') {
        var test = loc.state.markers[0];
        console.log(test.lat);
        console.log(test.lng);

        const redirectLoc = {
            Longitude: '' + test.lng,
            Latitude: '' + test.lat,
            MapUrl: '',
        };
        //console.log({redirectLoc})
        setLocation(redirectLoc);
    }

    //console.log({loc});

    const filePicker = useRef(null);

    async function postMsg(e) {
        const result = handleData(e);

        axios
            .post('https://localhost:44312/api/Message/message', result, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    function handleData(e) {
        const formData = new FormData();
        formData.append('Title', data.Title);
        formData.append('Description', data.Description);
        formData.append('Data', data.Data);
        formData.append('PhoneNumber', data.PhoneNumber);
        formData.append('Attachment', file);
        formData.append('Longitude', location.Longitude);
        formData.append('Latitude', location.Latitude);
        formData.append('MapUrl', location.MapUrl);

        return formData;
    }

    const handlePick = () => {
        filePicker.current.click();
    };

    function handle(e) {
        const newData = { ...data };
        newData[e.target.id] = e.target.value;
        loadData(newData);
        console.log(newData);
    }

    function handleFile(e) {
        setFile(e.target.files[0]);
    }

    function handleLocation(e) {
        let lat = '0';
        let lon = '0';
        let url = '';
        console.log(e);

        if (e.Latitude !== undefined) {
            lat = e.Latitude;
        }

        if (e.Longitude !== undefined) {
            lon = e.Longitude;
        }

        if (e.MapUrl !== undefined) {
            url = e.MapUrl;
        }

        const location = {
            Longitude: lon,
            Latitude: lat,
            MapUrl: url,
        };
        setLocation(location);
    }

    return (
        <>
            <Form onSubmit={postMsg}>
                <h1>Create important message</h1>
                <Block>
                    <label>
                        Title
                        <input
                            id="Title"
                            name="Title"
                            onChange={e => handle(e)}
                            value={data.Title}
                            required
                        />{' '}
                    </label>
                    <label>
                        Phone
                        <input
                            id="PhoneNumber"
                            name="PhoneNumber"
                            pattern="-?[0-9]*(\.[0-9]{6,32})?"
                            maxLength={14}
                            onChange={e => handle(e)}
                            value={data.PhoneNumber}
                            type="tel"
                            placeholder="+380-99-77-77-777"
                            required
                        />
                    </label>
                    <label>
                        Data
                        <input
                            id="Data"
                            name="Data"
                            onChange={e => handle(e)}
                            value={data.Data}
                            type={'datetime-local'}
                            required
                        />
                    </label>
                    <label>
                        Description
                        <textarea
                            id="Description"
                            name="Description"
                            onChange={e => handle(e)}
                            value={data.Description}
                        />
                    </label>
                    <Block>
                        <label>Add Location</label>
                        <SmButton
                            type="button"
                            onClick={handleLocation}
                            disabled>
                            Pin Location
                        </SmButton>
                        <span>{JSON.stringify(location)}</span>
                    </Block>
                    <Block>
                        <label htmlFor="images">
                            Add File
                            <input
                                id="Attachment"
                                name="Attachment"
                                className="hidden"
                                onChange={handleFile}
                                type="file"
                                accept="image/*"
                                ref={filePicker}
                            />
                        </label>
                        <SmButton type="button" onClick={handlePick}>
                            PinFile
                        </SmButton>
                    </Block>
                    <Block>
                        <label>Submit</label>
                        <BgButton type="submit">Submit</BgButton>
                    </Block>
                </Block>
            </Form>
        </>
    );
};

export default MessageForm;
