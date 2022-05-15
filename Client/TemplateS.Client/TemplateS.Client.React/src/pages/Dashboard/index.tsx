/* eslint-disable jsx-a11y/anchor-is-valid */
import { useState, useEffect } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { useLazyQuery } from '@apollo/client';
import { FiChevronRight, FiLogOut, FiLogIn, FiTrash2 } from 'react-icons/fi';
import { GET_REPOSITORY, PROFILE_QUERY } from '../../graphql/pullrequest-queries';

import { apolloClearCache } from '../../App';

import { Header, PRImg, SubHeader, Title, Form, Error, Repositories } from './styles';

import pullRequestSvg from '../../assets/pr.svg';

interface IRepository {
    id: string;
    owner: {
        avatarUrl: string;
        login: string;
    };
    name: string;
    nameWithOwner: string;
    description: string;
}

const Dashboard: React.FC = () => {
  const history = useHistory();

  const [newToken, setNewToken] = useState('');
  const [messageError, setMessageError] = useState('');
  const [newRepo, setNewRepo] = useState('');
  const [hasToken, setHasToken] = useState(() => {
    const token = localStorage.getItem(process.env.REACT_APP_LOCALSTORAGE_PROPERTY_NAME);

    if(!token) {
      localStorage.removeItem('@PR-MANAGER:repositories');
    }

    return !!token;
  });
  const [repositories, setRepositories] = useState<IRepository[]>(() => {
    const storageRepositories = localStorage.getItem('@PR-MANAGER:repositories');

    if(storageRepositories) {
      return JSON.parse(storageRepositories);
    }

    return [];
  });
  const defaultMessage = 'The no API request returned an error.';
  const unauthorized = 'status code 401';
  const handleSuccessGetRepository = (repository: IRepository | null) => {
    setHasToken(true);
    setMessageError('');

    if(repository) {
      setRepositories([...repositories, repository]);
    } else if (newRepo) {
      setMessageError('No repository found with this name.');
    }

    clearInputs();
  };
  const handleMessageError = (message: string) => {
    if (message.includes(unauthorized)) {
        setMessageError(`${defaultMessage} Unable to authorize with the given token.`)
        clearLocalStorage();
    } else {
        setMessageError(`${defaultMessage} Error: ${message}`)
    }

    clearInputs();
  };
  const [getRepositories] = useLazyQuery(GET_REPOSITORY, {
      variables: { name: '' },
      onCompleted: (data) => { handleSuccessGetRepository(data?.viewer?.repository) },
      onError: ({ message }) => { handleMessageError(message) },
  });
  const [getProfile] = useLazyQuery(PROFILE_QUERY, {
      onCompleted: () => { handleSuccessGetRepository(null) },
      onError: ({ message }) => { handleMessageError(message); },
  });

  useEffect(() => {
    if(repositories.length > 0) {
      localStorage.setItem('@PR-MANAGER:repositories', JSON.stringify(repositories));
    }
  }, [repositories]);

  const handleUseToken = (event: any) => {
    event.preventDefault();

    if(!newToken) {
      setMessageError('Put the token to authenticate in the GitHub API');
      clearInputs();
      return;
    }

    localStorage.setItem(process.env.REACT_APP_LOCALSTORAGE_PROPERTY_NAME, newToken);

    getProfile();
  }

  const handleAddRepository = (event: any) => {
    event.preventDefault();

    if(!newRepo) {
      setMessageError('Enter the repository name');
      clearInputs();
      return;
    }

    getRepositories({variables: { name: newRepo }});
  }

  const handleResetToken = (event: any) => {
    event.preventDefault();

    clearLocalStorage();
    clearInputs();
    setMessageError('');
  }

  const handleResetRepo = (event: any) => {
    event.preventDefault();

    clearRepo();
  }

  const handleRepositoryClick = (avatarUrl: string, repositoryName: string) => {
    localStorage.setItem('@PR-MANAGER:avatarUrl', avatarUrl);

    history.push(`/repositories/${repositoryName}`);
  }

  const clearLocalStorage = () => {
    clearRepo();
    localStorage.removeItem(process.env.REACT_APP_LOCALSTORAGE_PROPERTY_NAME);
    setHasToken(false);
  }

  const clearRepo = () => {
    localStorage.removeItem('@PR-MANAGER:repositories');
    setRepositories([]);
  }

  const clearInputs = () => {
    setNewToken('');
    setNewRepo('');
  }

  return (
    <>
      <Header>
        <PRImg>
          <img src={pullRequestSvg}  alt="Pull request SVG"></img>
          <strong>Pull Request Manager</strong>
        </PRImg>

        {hasToken && (
          <a onClick={handleResetToken}>
              <FiLogOut size={20}/>
              Reset Token
          </a>
        )}
      </Header>

      <SubHeader>
        <Title>Multi Repo PR Manager</Title>

        {repositories.length > 0 &&
          <a onClick={handleResetRepo}>
            <FiTrash2 size={20}/>
            Reset Repo
          </a>
        }
      </SubHeader>

      <Form hasError={!!messageError} onSubmit={!hasToken ? handleUseToken : handleAddRepository}>
        <input
          value={!hasToken ? newToken : newRepo}
          onChange={e => (!hasToken ? setNewToken(e.target.value) : setNewRepo(e.target.value))}
          placeholder={!hasToken ? 'Enter your access token' : 'Enter your repository'}
        />
        <button type="submit">{!hasToken ? 'Use' : 'Search'}</button>
      </Form>

      {messageError && <Error>{messageError}</Error>}

      <Repositories>
        {repositories &&
          repositories.map(repository => (
            <a key={repository.id} onClick={() => handleRepositoryClick(repository.owner.avatarUrl, repository.name)}>
              <img src={repository.owner.avatarUrl} alt={repository.owner.login} />

              <div>
                  <strong>{repository.nameWithOwner}</strong>
                  <p>{!!repository.description ? repository.description : "Sem descrição"}</p>
              </div>

              <FiChevronRight size={20} />
            </a>
          )
        )}
      </Repositories>
    </>
  );
}

export default Dashboard;
