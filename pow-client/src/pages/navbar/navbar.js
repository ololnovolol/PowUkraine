import React, { useState } from 'react';
import styled from 'styled-components';
import { Link } from 'react-router-dom';
import * as FiIcons from 'react-icons/cg';
import { SidebarData } from './sidebar';
import SubMenu from './subMenu';
import { IconContext } from 'react-icons/lib';

const Nav = styled.div`
  background: #3A5431;
  height: 80px;
  display: flex;
  justify-content: flex-start;
  align-items: center;
  position: relative;
  left: 10;
  right: 100;
  box-sizing: border-box;
  outline: currentcolor none medium;
  max-width: 100%;
  margin: 0px;
  align-items: center;
  background: #3A5431 none repeat scroll 0% 0%;
  color: rgb(248, 248, 248);
  min-width: 0px;
  min-height: 0px;
  flex-direction: row;
  justify-content: flex-start;
  padding: 0px 0px
  box-shadow: rgba(0, 0, 0, 0.2) 0px 10px 20px;
  z-index: 0;
`;

const NavIcon = styled(Link)`
    margin-left: 3rem;
    font-size: 3rem;
    height: 80px;
    display: flex;
    justify-content: flex-start;
    align-items: center;
`;

const SidebarNav = styled.nav`
    background: #3a5431 none repeat scroll 0% 0%;
    width: 280px;
    height: 100vh;
    display: flex;
    justify-content: flex-start;
    position: absolute;
    top: 0;
    left: ${({ sidebar }) => (sidebar ? '0' : '-100%')};
    transition: 350ms;
    z-index: 10;
    bottom: 0px;
    z-index: 293;
    display: block;
    max-width: 100%;
    align-items: stretch;
    color: rgb(248, 248, 248);
    min-width: 0px;
    min-height: 0px;
    flex-direction: row;
    padding: 0px 0px;
    box-shadow: rgba(0, 0, 0, 0.2) 0px 10px 20px;
    z-index: 400;
`;

const SidebarWrap = styled.div`
    width: 100%;
`;

const Sidebar = () => {
    const [sidebar, setSidebar] = useState(false);

    const showSidebar = () => setSidebar(!sidebar);

    return (
        <>
            <IconContext.Provider value={{ color: '#F0A30A' }}>
                <Nav>
                    <NavIcon to="#">
                        <FiIcons.CgMenu onClick={showSidebar} />
                    </NavIcon>
                </Nav>
                <SidebarNav sidebar={sidebar}>
                    <SidebarWrap>
                        <NavIcon to="#">
                            <FiIcons.CgMenuMotion onClick={showSidebar} />
                        </NavIcon>
                        {SidebarData.map((item, index) => {
                            return <SubMenu item={item} key={index} />;
                        })}
                    </SidebarWrap>
                </SidebarNav>
            </IconContext.Provider>
        </>
    );
};

export default Sidebar;
