import styled, { css } from 'styled-components';
import { shade } from 'polished';

export const Container = styled.div`
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    width: 320px;
    background-color: #fff;
`;

export const Header = styled.div`
    display: grid;
    place-items: center;
    height: 120px;
    font-size: 1.5rem;
    font-weight: 700;
    color: #3A3A3A;
`;

export const Menu = styled.div`
    position: relative;

    a {
        text-decoration: none;
    }
`;

export const MenuIndicator = styled.div`
    position: absolute;
    top: 0;
    left: 50%;
    width: calc(100% - 40px);
    border-radius: 10px;
    z-index: -1;
`;

interface IMenuItemProps {
    active: boolean;
}

export const MenuItem = styled.div<IMenuItemProps>`
    display: flex;
    align-items: center;
    place-content: flex-start;
    padding: 1rem 3rem;
    font-size: 1.25rem;
    font-weight: 500;
    color: #a8a8b3;
    transition: color 0.2s;
    transition: transform 0.2s;
    cursor: pointer;

    ${props => props.active && css`color: ${shade(0.5, '#a8a8b3')}`};

    svg {
        margin-right: 1rem;
        font-size: 1.75rem;
        transition: transform 0.2s;
    }

    &:hover {
        color: ${shade(0.2, '#a8a8b3')};
        transform: translateX(5%);
    }
`;

export const MenuItemText = styled.div`
    font-size: 18px;
    transition: transform 0.2s;
`;