/* eslint-disable jsx-a11y/anchor-is-valid */
/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useRef, useState, useMemo } from 'react';
import { useHistory, useLocation } from 'react-router-dom';
import { FiHome, FiLogIn, FiUsers, FiLogOut } from 'react-icons/fi';
import { BiBracket } from 'react-icons/bi';
import { MdOutlineContacts } from 'react-icons/md';
import { IoIosContacts } from 'react-icons/io';
import { FaCity } from 'react-icons/fa';

import { Container, Header, Menu, MenuIndicator, MenuItem, MenuItemText } from './styles';

import { useAuth } from '../../hooks/auth';

interface INavItem {
    display: string;
    icon: React.ReactNode;
    to: string;
    visible: boolean;
    handler: Function;
}

interface INavItemsObject {
    [key: string]: INavItem;
}

const Sidebar: React.FC = () => {
    const { signOut, user } = useAuth();
    const history = useHistory();

    const [menu, setMenu]  = useState<INavItemsObject>({
        Dashboard: {
            display: 'Dashboard',
            icon: <FiHome />,
            to: '/',
            handler: (thisObj: INavItem) => history.push(thisObj.to),
            visible: false
        },
        DashboardRepository: {
            display: 'Pull Requests',
            icon: <FiHome />,
            to: '/databaserepositories',
            handler: (thisObj: INavItem) => history.push(thisObj.to),
            visible: false
        },
        Users: {
            display: 'Users',
            icon: <FiUsers />,
            to: '/users',
            handler: (thisObj: INavItem) => history.push(thisObj.to),
            visible: false
        },
        Contacts: {
            display: 'Contacts',
            icon: <MdOutlineContacts />,
            to: '/contacts',
            handler: (thisObj: INavItem) => history.push(thisObj.to),
            visible: false
        },
        Persons: {
            display: 'Persons',
            icon: <IoIosContacts />,
            to: '/persons',
            handler: (thisObj: INavItem) => history.push(thisObj.to),
            visible: false
        },
        Cities: {
            display: 'Cities',
            icon: <FaCity />,
            to: '/cities',
            handler: (thisObj: INavItem) => history.push(thisObj.to),
            visible: false
        },
        BalancedBrackets: {
            display: 'Balanced Brackets',
            icon: <BiBracket />,
            to: '/balancedbrackets',
            handler: (thisObj: INavItem) => history.push(thisObj.to),
            visible: true
        },
        SignIn: {
            display: 'SignIn',
            icon: <FiLogIn />,
            to: '/signin',
            handler: (thisObj: INavItem) => history.push(thisObj.to),
            visible: false
        },
        SignOut: {
            display: 'SignOut',
            icon: <FiLogOut />,
            to: '/dashboard',
            handler: () => signOut(),
            visible: false
        },
    });

    const [activeMenu, setActiveMenu] = useState('');
    const sidebarRef = useRef<HTMLInputElement>(null);
    const indicatorRef = useRef<HTMLInputElement>(null);
    const location = useLocation();

    useEffect(() => {
        const newMenu = menu;

        //deslogado
        newMenu['SignIn'].visible = !user;
        newMenu['Dashboard'].visible = !user;

        //logado
        newMenu['SignOut'].visible = !!user;
        newMenu['DashboardRepository'].visible = !!user;
        newMenu['Users'].visible = !!user;
        newMenu['Contacts'].visible = !!user;
        newMenu['Persons'].visible = !!user;
        newMenu['Cities'].visible = !!user;

        setMenu({ ...menu, ...newMenu });
    }, [user, location]);

    useEffect(() => {
        const curPath = window.location.pathname.split('/')[1];
        const activeItem = menuArray.find(item => item.to === `/${curPath}`)?.display;
        setActiveMenu(curPath.length === 0 ? '' : activeItem!);
    }, [location]);

    const menuArray = useMemo(() => {
        return Object.values(menu)
    }, [menu]);

    return (
        <Container>
            <Header>Menu</Header>
            <Menu ref={sidebarRef}>
                <MenuIndicator ref={indicatorRef} />
                {menuArray.filter(f => f.visible).map((item, index) => (
                    <a key={index} onClick={() => item.handler(item)}>
                        <MenuItem active={activeMenu === item.display}>
                            {item.icon}
                            <MenuItemText>{item.display}</MenuItemText>
                        </MenuItem>
                    </a>
                ))}
            </Menu>
        </Container>
    );
};

export default Sidebar;