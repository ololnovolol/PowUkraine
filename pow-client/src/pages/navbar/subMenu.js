import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';

const SidebarLink = styled(Link)`
    display: flex;
    color: #ffffff;
    justify-content: space-between;
    align-items: center;
    padding: 20px;
    list-style: none;
    height: 80px;
    text-decoration: none;
    font-size: 18px;
    &:hover {
        color: #f0a30a;
        background: #6d8764;
        border-left: 4px solid #f0a30a;
        cursor: pointer;
    }
`;

const SidebarLabel = styled.span`
    margin-left: 24px;
`;

const DropdownLink = styled(Link)`
    background: #f0a30a;
    height: 60px;
    padding-left: 3rem;
    display: flex;
    align-items: center;
    text-decoration: none;
    color: #ffffff;
    font-size: 18px;
    &:hover {
        color: #f0a30a;
        background: #6d8764;
        cursor: pointer;
    }
`;

const SubMenu = ({ item }) => {
    const [subnav, setSubnav] = useState(false);

    const showSubnav = () => setSubnav(!subnav);

    return (
        <>
            <SidebarLink to={item.path} onClick={item.subNav && showSubnav}>
                <div>
                    {item.icon}
                    <SidebarLabel>{item.title}</SidebarLabel>
                </div>
                <div>
                    {item.subNav && subnav
                        ? item.iconOpened
                        : item.subNav
                        ? item.iconClosed
                        : null}
                </div>
            </SidebarLink>
            {subnav &&
                item.subNav.map((item, index) => {
                    return (
                        <DropdownLink to={item.path} key={index}>
                            {item.icon}
                            <SidebarLabel>{item.title}</SidebarLabel>
                        </DropdownLink>
                    );
                })}
        </>
    );
};

export default SubMenu;
