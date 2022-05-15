/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable jsx-a11y/anchor-is-valid */
import { useEffect, useState } from 'react';
import { FiUsers, FiUser, FiChevronRight } from 'react-icons/fi';

import { Header, HeaderContent, UsersContent, SubHeader, Title } from './styles';

import { useAuth } from '../../hooks/auth';

import api from '../../services/api';

interface IUser {
    id: string;
    name: string;
    email: string;
    password: string;
}

const Users: React.FC = () => {
  const { user } = useAuth();

  const [users, setUsers] = useState<IUser[]>([]);

  useEffect(() => {
    api
      .get('users')
      .then(({ data }) => {
        setUsers([...users, ...data.datas]);
      });
  }, [user.id]);

  const handleClickUser = () => {

  };

  return (
    <>
      <Header>
        <HeaderContent>
          <FiUsers size={24} />
          <strong>Users</strong>
        </HeaderContent>
      </Header>

      <SubHeader>
        <Title>Wellcome {user.name}</Title>
      </SubHeader>

      <UsersContent>
        {users &&
            users.map(user => (
                <a key={user.id} onClick={handleClickUser}>
                    <FiUser />
                    
                    <div>
                        <strong>{user.name}</strong>
                        <p>{user.email}</p>
                    </div>

                    <FiChevronRight size={20} />
                </a>
            ))
        }
      </UsersContent>
    </>
  );
}

export default Users;
