import styled, { css }from 'styled-components';
import { shade } from 'polished';

export const Header = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  max-width: 700px;
`;

export const HeaderContent = styled.div`
  display: flex;
  align-items: center;

  svg {
    width: 22px;
    margin-right: 20px;
  }

  strong {
    font-weight: bold;
  }
`;

export const SubHeader = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  max-width: 700px;
`;

export const Title = styled.h1`
  font-size: 48px;
  color: #3A3A3A;
  max-width: 450px;
  line-height: 56px;

  margin-top: 80px;
`;

interface IFormProps {
  hasError: boolean;
}

export const Form = styled.form<IFormProps>`
  margin-top: 40px;
  max-width: 700px;

  display: flex;

  input {
    flex: 1;
    height: 70px;
    padding: 0 24px;
    border: 0;
    border-radius: 5px 0 0 5px;
    color: #3A3A3A;
    border: 2px solid #fff;
    border-right: 0;

    ${props => props.hasError && css`
      border-color: #c53030;
    `}

    &::placeholder {
      color: #a8a8b3;
    }
  }

  button {
    width: 210px;
    height: 70px;
    background: #04D361;
    border-radius: 0 5px 5px 0;
    border: 0;
    color: #fff;

    transition: background-color 0.2s;

    &:hover {
      cursor: pointer;
      background: ${shade(0.2, '#04D361')};
    }
  }
`;

export const Error = styled.span`
  display: block;
  color: #c53030;
  margin-top: 8px;
`;