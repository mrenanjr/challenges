/* eslint-disable jsx-a11y/anchor-is-valid */
/* eslint-disable jsx-a11y/img-redundant-alt */
/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from 'react';
import { useHistory, useRouteMatch } from 'react-router-dom';
import { FiChevronLeft, FiChevronRight, FiClipboard, FiDatabase } from 'react-icons/fi';
import { useQuery } from '@apollo/client';
import { GET_PULLREQUESTS } from '../../graphql/pullrequest-queries';

import { Header, PRImg, RepositoryInfo, PullRequests, PullR } from './styles';

import pullRequestSvg from '../../assets/pr.svg';
import api from '../../services/api';
import { useToast } from '../../hooks/toast'

interface IMatchParams {
    repository: string;
}

interface IPullRequest {
    login?: string;
    id?: string;
    githubid?: string;
    title: string;
    author: {
        login: string;
    };
    state: string;
    url: string;
}

interface IApolloRefetchVars {
    name: string;
    first: number;
    after: string | null;
}

const Repository: React.FC = () => {
  const match = useRouteMatch<IMatchParams>();

  const { addToast } = useToast();
  const history = useHistory();

  const avatarUrl = localStorage.getItem('@PR-MANAGER:avatarUrl')!;
  const [colaborators, setColaborators] = useState(0);
  const [forks, setForks] = useState(0);
  const [issues, setIssues] = useState(0);
  const [pullRequestsCount, setPullRequestsCount] = useState(0);
  const [description, setDescription] = useState(null);

  const [pullRequests, setPullRequests] = useState<IPullRequest[]>([]);
  const [pullRequestsInDB, setPullRequestsInDB] = useState<string[]>([]);
  const [hasNextPage, setHasNextPage] = useState(false);
  const [endCursor, setEndCursor] = useState(null);

  const { data, refetch } = useQuery<any, IApolloRefetchVars>(GET_PULLREQUESTS, {
    variables: { name: match.params.repository, first: 5, after: null },
    fetchPolicy: "no-cache"
  });

  useEffect(() => {
    if(data) {
      const repository = data.viewer.repository;
      setColaborators(repository.collaborators.totalCount);
      setIssues(repository.issues.totalCount);
      setDescription(repository.description);
      setForks(repository.forkCount);

      if(repository.pullRequests) {
        setPullRequestsCount(repository.pullRequests.totalCount);
        setHasNextPage(repository.pullRequests.pageInfo.hasNextPage);
        setEndCursor(repository.pullRequests.pageInfo.endCursor);
        const nodes = repository.pullRequests.nodes.map((node: any) => ({ ...node, githubid: node.id, login: node.author?.login }));
        setPullRequests([...pullRequests, ...nodes ]);
      }
    }
  }, [data]);

  useEffect(() => {
    api
      .get('/pullrequests')
      .then(({ data }) => {
        setPullRequestsInDB([ ...pullRequestsInDB, ...data.datas.map((m: any) => m.githubid!) ]);
      });
  }, []);

  useEffect(() => {
    if(pullRequests && pullRequestsInDB) {
      const ids = pullRequestsInDB.map((m: any) => m.id);
      
      pullRequests.map(m => {
        var insideDb = ids.includes(m.id);
        
        return insideDb ? { ...m, insideDb } : m;
      });

    }
  }, [pullRequests, pullRequestsInDB]);

  const handleCopyLinks = () => {
    const textField = document.createElement('textarea');

    textField.innerHTML = pullRequests.map(pr => pr.url).join('\r\n');
    document.body.appendChild(textField);
    textField.select();
    document.execCommand('copy');
    textField.remove();
  };

  const handleNextPage = (event: any) => {
    event.preventDefault();
    refetch({
        name: match.params.repository,
        first: 5,
        after: endCursor
    });
  };

  const handlePRToDatabase = async (id: string) => {
    try {
      const data = pullRequests.find(f => f.id === id)!;

      await api.post('/pullrequests', data);

      addToast({
        type: 'success',
        title: 'Data sent successfully!'
      });

      setPullRequestsInDB([...pullRequestsInDB, data.githubid! ]);
    } catch (err) {
      addToast({
        type: 'error',
        title: 'Unable to add data to database',
        description: 'There was an error sending the PR data to the base, please try again later.',
      });
    }
  };

  const handleClickBack = (event: any) => {
    event.preventDefault();
    localStorage.removeItem('@PR-MANAGER:avatarUrl');
    history.push('/dashboard');
  };

  return (
    <>
      <Header>
        <PRImg>
          <img src={pullRequestSvg}  alt="Pull request SVG"></img>
          <strong>Pull Request Manager</strong>
        </PRImg>

        <a onClick={handleClickBack}>
          <FiChevronLeft size={20}/>
          Back
        </a>
      </Header>

      <RepositoryInfo>
        <header>
          <img src={avatarUrl} alt="Avatar image"></img>
          <div className="infoContainer">
            <div className="info">
              <strong>{match.params.repository}</strong>
              <p>{!!description ? description : "Sem descrição"}</p>
            </div>

            {pullRequests &&
              <a onClick={handleCopyLinks}>
                <FiClipboard size={20}/>
                Copy Links
              </a>
            }
          </div>
        </header>

        <ul>
          <li>
            <strong>{colaborators}</strong>
            <span>Contributors</span>
          </li>
          <li>
            <strong>{forks}</strong>
            <span>Forks</span>
          </li>
          <li>
            <strong>{issues}</strong>
            <span>Open Issues</span>
          </li>
          <li>
            <strong>{pullRequestsCount}</strong>
            <span>{pullRequestsCount > 1 ? "Pull Requests" : "Pull Request"} </span>
          </li>
        </ul>
      </RepositoryInfo>

      <PullRequests>
        {pullRequests &&
          pullRequests.map(pullRequest => (
            <PullR
              key={pullRequest.id}
              onClick={() => handlePRToDatabase(pullRequest.githubid!)}
              inDB={pullRequestsInDB.includes(pullRequest.githubid!)}
            >
              <img src={pullRequestSvg}  alt="Pull request SVG"></img>

              <div>
                  <strong>{pullRequest.title}</strong>
                  <p>{pullRequest.author.login}</p>
                  <p>{pullRequest.state}</p>
                  <strong>{pullRequest.url}</strong>
              </div>

              <FiDatabase size={20} />
            </PullR>
          ))
        }
        {hasNextPage &&
          <a className="next" onClick={handleNextPage}>
            <FiChevronRight size={20}/>
            Next
          </a>
        }
      </PullRequests>
    </>
  );
}

export default Repository;
