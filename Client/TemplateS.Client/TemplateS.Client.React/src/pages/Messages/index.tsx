/* eslint-disable jsx-a11y/anchor-is-valid */
import { useEffect, useState } from 'react';

import { Header, HeaderContent, SubHeader, Title, Form, Error, MessagesContent } from './styles';

import api from '../../services/api';
import { useToast } from '../../hooks/toast';
import { FaCloudDownloadAlt, FaRocketchat } from 'react-icons/fa';
import moment from 'moment';
import { FiChevronRight } from 'react-icons/fi';

interface IMessage {
  fromId: number;
  toId: number;
  content: string;
  createdAt: Date;
}

const Messages: React.FC = () => {
  const { addToast } = useToast();

  const [input, setInput] = useState('');
  const [message, setMessage] = useState('');
  const [messages, setMessages] = useState<IMessage[]>(() => {
    const storageMessages = localStorage.getItem('@PR-MANAGER:messages');

    if(storageMessages) {
      return JSON.parse(storageMessages);
    }

    return [];
  });

  useEffect(() => {
    if(messages) {
      localStorage.setItem('@PR-MANAGER:messages', JSON.stringify(messages));
    }
  }, [messages]);

  const handleSendMessage = async (event: any) => {
    event.preventDefault();

    if(!input) {
      setMessage('Inform message to send to RabbitMQ.');
      return;
    }

    try {
      debugger;
      let model = { content: input, createdAt: new Date() };
      const { data } = await api.post(`/messages`, model);

      addToast({
          type: data.message?.includes('not') ? 'error' : 'success',
          title: 'Message successfully sended to RabbitMQ.',
          description: data.message
      });
    } catch (err: any) {
      let message = err?.response?.data?.error;

      if(!message) message = err?.message;

      addToast({
          type: 'error',
          title: 'Unable to send message to RabbitMQ.',
          description: message
      });
    }

    setInput('');
    setMessage('');
  }

  const handleConsumeMessages = async (event: any) => {
    event.preventDefault();

    try {
      debugger;
      const { data } = await api.get(`/messages/consumemessage`);
      let auxMessages = '';

      if(data.data) {
        let auxMessages = [...messages, data.data];
        
        localStorage.setItem('@PR-MANAGER:messages', JSON.stringify(auxMessages));
        setMessages(auxMessages);
      } else {
        auxMessages = ' But there were none.';
      }

      addToast({
        type: data.message?.includes('not') ? 'error' : auxMessages ? 'info' : 'success',
        title: `Messages consumed succesfully.${auxMessages}`,
        description: data.message
      });
    } catch (err: any) {
      let message = err?.response?.data?.error;

      if(!message) message = err?.message;

      addToast({
          type: 'error',
          title: 'Unable to consume messages.',
          description: message
      });
    }

    setInput('');
    setMessage('');
  }

  const formatDate = (date: Date): string => moment(date).format('DD/MM/yyyy HH:mm');

  return (
    <>
      <Header>
        <HeaderContent>
          <FaRocketchat size={24} />
          <strong>RabbitMQ Messages</strong>
        </HeaderContent>
      </Header>

      <SubHeader>
        <Title>RabbitMQ Messages Manager</Title>

        <a onClick={handleConsumeMessages}>
          <FaCloudDownloadAlt size={20}/>
          Consume
        </a>
      </SubHeader>

      <Form hasError={!!message} onSubmit={handleSendMessage}>
        <input
          value={input}
          onChange={e => setInput(e.target.value)}
          placeholder={'Enter new message'}
        />
        <button type="submit">Send</button>
      </Form>

      {message && <Error>{message}</Error>}

      <MessagesContent>
        {messages &&
            messages.map((msg, index) => (
                <a key={`${index}-${msg.fromId}-${msg.toId}`} onClick={() => {}}>
                    <FaRocketchat size={24} />
                    
                    <div>
                        <strong>From: {msg.fromId} | To: {msg.toId}</strong>
                        <p>{msg.content}</p>
                        <strong>{formatDate(msg.createdAt)}</strong>
                    </div>

                    <FiChevronRight size={20} />
                </a>
            ))
        }
      </MessagesContent>
    </>
  );
}

export default Messages;