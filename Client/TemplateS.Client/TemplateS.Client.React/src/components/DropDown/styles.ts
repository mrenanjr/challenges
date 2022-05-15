import styled, { css } from 'styled-components';

interface IContentProps {
    isErrored: boolean;
}

export const Content = styled.div<IContentProps>`
    .bg-white {
        padding: 16px;
        border-radius: 10px;
        border: 0;

        &:focus {
            box-shadow: none;
        }

        ${props =>
            props.isErrored &&
            css`
                border: 2px solid #c53030;
            `
        }
    }

    .dropdown-menu {
        button {
            text-align: center;
        }

        .dropdown-item {
            &:active {
                background-color: #03a84d;
            }
        }
    }

    .dropdown-menu .active {
        background-color: #03a84d;
    }
`;

export const DropdownToggleContent = styled.div`
    display: flex;
    width: inherit;
    justify-content: space-between;
    align-items: center;
`;