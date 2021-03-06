import { BrowserRouter } from "react-router-dom";
import { ApolloProvider } from '@apollo/client';

import GlobalStyle from './styles/global';
import Routes from "./routes";

import apolloClient from './services/apollo';
import client from "./services/apollo";

import AppProvider from './hooks';
import Sidebar from './components/Sidebar';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;

export const apolloClearCache = () => {
  client.resetStore();
}

const App: React.FC = () => (
  <BrowserRouter basename={baseUrl}>
      <ApolloProvider client={apolloClient}>
          <AppProvider>
            <Sidebar />
            <div id="globalContent">
              <Routes />
            </div>
          </AppProvider>

          <GlobalStyle />
      </ApolloProvider>
  </BrowserRouter>
);

export default App;

