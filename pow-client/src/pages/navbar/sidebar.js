import React from 'react';
import * as FaIcons from 'react-icons/fa';
import * as MdIcons from 'react-icons/md';
import * as IoIcons from 'react-icons/io';
import * as RiIcons from 'react-icons/ri';
import * as TiIcons from 'react-icons/ti';

export const SidebarData = [
    {
        title: 'Mark the enemy',
        role: 'undefined',
        path: '/',
        icon: <FaIcons.FaMapMarkerAlt />,
        cName: 'nav-text',
    },
    {
        title: 'Important message',
        role: 'undefined',
        path: '/message',
        icon: <RiIcons.RiMessage3Line />,
    },

    {
        title: '',
        path: '',
        icon: '',
        cName: 'nav-text',
    },
    {
        title: '',
        path: '',
        icon: '',
        cName: 'nav-text',
    },
    {
        title: '',
        path: '',
        icon: '',
        cName: 'nav-text',
    },
    {
        title: '',
        path: '',
        icon: '',
        cName: 'nav-text',
    },
    {
        title: '',
        path: '',
        icon: '',
        cName: 'nav-text',
    },
    {
        title: '',
        path: '',
        icon: '',
        cName: 'nav-text',
    },

    {
        title: 'Admin panel',
        role: 'admin',
        path: '',
        icon: <RiIcons.RiAdminLine />,
        iconClosed: <MdIcons.MdOutlineKeyboardArrowDown />,
        iconOpened: <MdIcons.MdKeyboardArrowUp />,

        subNav: [
            {
                title: 'Manage messages',
                path: '/manageMessages',
                icon: <TiIcons.TiMessages />,
            },
            {
                title: 'Accounts',
                path: '/manageAccounts',
                icon: <IoIcons.IoMdPeople />,
            },
            {
                title: 'Logout',
                path: '/logout',
                icon: <RiIcons.RiLogoutBoxRLine />,
            },
        ],
    },
    {
        title: 'Lobby',
        role: 'user',
        path: '',
        icon: <RiIcons.RiUserSettingsLine />,
        iconClosed: <MdIcons.MdOutlineKeyboardArrowDown />,
        iconOpened: <MdIcons.MdKeyboardArrowUp />,

        subNav: [
            {
                title: 'All marks',
                path: '/manageMarks',
                icon: <MdIcons.MdOutlineBookmarks />,
            },
            {
                title: 'Manage Account',
                path: '/manageAccount',
                icon: <RiIcons.RiSettings4Line />,
            },
            {
                title: 'Logout',
                path: '/logout',
                icon: <RiIcons.RiLogoutBoxRLine />,
            },
        ],
    },
    {
        title: 'Authorization',
        role: 'undefined',
        path: '/login',
        icon: <RiIcons.RiOpenArmLine />,
        cName: 'nav-text',
    },
    {
        title: 'About',
        role: 'undefined',
        path: '/about',
        icon: <IoIcons.IoMdHelpCircle />,
        cName: 'nav-text',
    },
];
