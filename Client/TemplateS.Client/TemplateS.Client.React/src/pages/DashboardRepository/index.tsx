/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable jsx-a11y/anchor-is-valid */
import { useEffect, useState } from 'react';
import { FiChevronRight, FiTrash2 } from 'react-icons/fi';

import { Header, PRImg, PullRequests, SubHeader, Title } from './styles';

import { useAuth } from '../../hooks/auth';
import { useToast } from '../../hooks/toast'

import api from '../../services/api';
import pullRequestSvg from '../../assets/pr.svg';

interface IPullRequest {
    login: string;
    id: string;
    github: string;
    title: string;
    state: string;
    url: string;
}

const DatabaseRepository: React.FC = () => {
  const { user } = useAuth();
  const { addToast } = useToast();

  const [pullRequests, setPullRequests] = useState<IPullRequest[]>([]);

  useEffect(() => {
    api
      .get('pullrequests')
      .then(({ data }) => {
        setPullRequests([...pullRequests, ...data.datas]);
      });
  }, [user.id]);

  const handleResetRepo = async (event: any) => {
    event.preventDefault();

    try {
      await api.delete('pullrequests/deleteall');

      setPullRequests([]);

      addToast({
        type: 'success',
        title: 'Data successfully removed!'
      });
    } catch (err) {
      addToast({
        type: 'error',
        title: 'Unable to remove data from base',
        description: 'An error occurred while trying to remove the PR from the base, please try again later.',
      });
    }
  }

  return (
    <>
      <Header>
        <PRImg>
          <img src={pullRequestSvg}  alt="Pull request SVG"></img>
          <strong>Database Pull Requests</strong>
        </PRImg>
      </Header>

      <SubHeader>
        <Title>Wellcome {user.name}</Title>

        {pullRequests.length > 0 &&
          <a onClick={handleResetRepo}>
            <FiTrash2 size={20}/>
            Reset Repo
          </a>
        }
      </SubHeader>

      <PullRequests>
        {pullRequests &&
          pullRequests.map(pullRequest => (
            <a key={pullRequest.id} target="_blank" href={`${pullRequest.url}`} rel="noreferrer">
              <img src={pullRequestSvg}  alt="Pull request SVG"></img>

              <div>
                  <strong>{pullRequest.title}</strong>
                  <p>{pullRequest.login}</p>
                  <p>{pullRequest.state}</p>
                  <strong>{pullRequest.url}</strong>
              </div>

              <FiChevronRight size={20} />
            </a>
          ))
        }
      </PullRequests>
    </>
  );
}

export default DatabaseRepository;
