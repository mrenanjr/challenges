import styled from 'styled-components';

export const Header = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
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
`;

export const Title = styled.h1`
  font-size: 48px;
  color: #3A3A3A;
  max-width: 450px;
  line-height: 56px;

  margin-top: 80px;
`;

export const UsersContent = styled.div`
  margin-top: 80px;
  transition: color 0.2s;
  cursor: pointer;

  a {
    background: #fff;
    border-radius: 5px;
    width: 100%;
    padding: 24px;
    display: block;
    text-decoration: none;

    display: flex;
    align-items: center;
    transition: transform 0.2s;

    & + a {
      margin-top: 16px;
    }

    &:hover {
      transform: translateX(10px);
    }

    img {
      width: 22px;
    }

    div {
      margin-left: 16px;
      flex: 1;

      strong {
        font-size: 20px;
        color: #3D3D4D;
      }

      p {
        font-size: 18px;
        color: #a8a8b3;
        margin-top: 4px;
        margin-bottom: 4px;
      }
    }

    svg {
      margin-left: auto;
      color: #cbcbd6;
    }
  }
`;
