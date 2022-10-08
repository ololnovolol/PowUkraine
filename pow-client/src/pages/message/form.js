import React, { useState, useRef } from 'react';
import styled from 'styled-components';
import { useLocation } from 'react-router-dom';
import axios from 'axios';
import * as FcIcons from 'react-icons/fc';
import '../../style/forms/form.css';
import * as userManager from '../../common/services/userService';

const SmButton = styled.button`
    background: #88a87d none repeat scroll 0% 0%;
    height: 40px;
    weight: 100px;
    border-color: #fff;
    color: #fff;
    border-radius: 5px;
    font-size: 16px;
    &:hover {
        color: #fff;
        background: #6d8764;
        cursor: pointer;
`;

const BgButton = styled.button`
    background: #88a87d none repeat scroll 0% 0%;
    height: 40px;
    weight: 100px;
    border-color: #fff;
    color: #f0a30a;
    border-radius: 5px;
    font-size: 16px;
    margin-top: 2rem;
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
    top: 1rem;
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
        setLocation(redirectLoc);
    }

    const filePicker = useRef(null);

    async function postMsg(e) {
        let Id = await userManager.GetUserId();
        const result = handleData(e, Id);

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

    function handleData(e, Id) {
        const formData = new FormData();
        formData.append('Title', data.Title);
        formData.append('Description', data.Description);
        formData.append('Data', data.Data);
        formData.append('PhoneNumber', data.PhoneNumber);
        formData.append('Attachment', file);
        formData.append('Longitude', location.Longitude);
        formData.append('Latitude', location.Latitude);
        formData.append('MapUrl', location.MapUrl);
        formData.append('UserId', Id);

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
                        <span className="span">
                            {location.Latitude === '0' ? (
                                ''
                            ) : (
                                <FcIcons.FcCheckmark />
                            )}
                        </span>
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
                            Pin File
                        </SmButton>
                        <span className="span">
                            {file.length < 1 ? '' : <FcIcons.FcCheckmark />}
                        </span>
                    </Block>
                    <div>
                        <BgButton type="submit">OK</BgButton>
                    </div>
                </Block>
            </Form>
        </>
    );
};

export default MessageForm;
