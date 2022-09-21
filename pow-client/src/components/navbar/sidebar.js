import React from 'react';
import * as FaIcons from 'react-icons/fa';
import * as MdIcons from 'react-icons/md';
import * as IoIcons from 'react-icons/io';
import * as RiIcons from 'react-icons/ri';
import * as TiIcons from 'react-icons/ti';


export const SidebarData = [
  {
    title: 'Mark the enemy',
    path: '/',
    icon: <FaIcons.FaMapMarkerAlt />,
    cName: 'nav-text'
  },
  {
    title: 'Important message',
    path: '/message',
    icon: <RiIcons.RiMessage3Line />,

    iconClosed: <MdIcons.MdOutlineKeyboardArrowDown />,
    iconOpened: <MdIcons.MdKeyboardArrowUp />,

    subNav: [
      {
        title: 'Message 1',
        path: '/message/message1',
        icon: <IoIcons.IoIosPaper />
      },
      {
        title: 'Message 2',
        path: '/message/message2',
        icon: <IoIcons.IoIosPaper />
      }
    ]
  },

  {
    title: '',
    path: '',
    icon: '',
    cName: 'nav-text'
  },
  {
    title: '',
    path: '',
    icon: '',
    cName: 'nav-text'
  },
  {
    title: '',
    path: '',
    icon: '',
    cName: 'nav-text'
  },
  {
    title: '',
    path: '',
    icon: '',
    cName: 'nav-text'
  },
  {
    title: '',
    path: '',
    icon: '',
    cName: 'nav-text'
  },
  {
    title: '',
    path: '',
    icon: '',
    cName: 'nav-text'
  },
  
  {
    title: 'Admin panel',
    path: '/admin',
    icon: <RiIcons.RiAdminLine />,  
    iconClosed: <MdIcons.MdOutlineKeyboardArrowDown />,
    iconOpened: <MdIcons.MdKeyboardArrowUp />,

    subNav: [
        {
            title: 'Accounts',
            path: '/ManageAccounts',
            icon: <IoIcons.IoMdPeople />,
        },
        {
            title: 'All marks',
            path: '/allmarks',
            icon: <MdIcons.MdOutlineBookmarks />,
        },
        {
            title: 'All messages',
            path: '/allmarks',
            icon: <TiIcons.TiMessages />,
        }
    ],
  },
  {
    title: 'Lobby',
    path: '/userLobby',
    icon: <RiIcons.RiUserSettingsLine />,  
    iconClosed: <MdIcons.MdOutlineKeyboardArrowDown />,
    iconOpened: <MdIcons.MdKeyboardArrowUp />,

    subNav: [
        {
            title: 'All Marks',
            path: '/allmarks',
            icon: <MdIcons.MdBookmarks />,
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
      }
    ],
  },
  {
    title: 'Authorization',
    path: '/login',
    icon: <RiIcons.RiOpenArmLine />,
    cName: 'nav-text'
  },
  {
    title: 'About',
    path: '/about',
    icon: <IoIcons.IoMdHelpCircle />,
    cName: 'nav-text'
  },
];