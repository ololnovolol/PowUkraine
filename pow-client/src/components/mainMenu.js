import React, { useRef, useContext } from 'react';
import styled from 'styled-components';
import useOnClickOutside from '../hooks/onClickOutside';
import { MenuContext } from '../context/navState';
import HamburgerButton from './burgerButton';
import { SideMenu } from './sideMenu';

const Navbar = styled.div`
  display: flex;
  position: relative;
  left: 10;
  right: 100;
  box-sizing: border-box;
  outline: currentcolor none medium;
  max-width: 0%;
  margin: 10px;
  align-items: center;
  background: #3A5431 none repeat scroll 0% 0%;
  color: rgb(248, 248, 248);
  min-width: 0px;
  min-height: 0px;
  flex-direction: row;
  justify-content: flex-start;
  padding: 0px 0px;
  box-shadow: rgba(0, 0, 0, 0.2) 0px 10px 20px;
  z-index: 400;
`;

const MainMenu = () => {
  const node = useRef();
  const { isMenuOpen, toggleMenuMode } = useContext(MenuContext);
  useOnClickOutside(node, () => {
    // Only if menu is open
    if (isMenuOpen) {
      toggleMenuMode();
    }
  });

  return (
    <header ref={node}>
      <Navbar>
        <HamburgerButton />

      </Navbar>
      <SideMenu />
    </header>
  );
};

export default MainMenu;