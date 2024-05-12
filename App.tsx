/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 */

import React, {useEffect} from 'react';
import {
  SafeAreaView,
  ScrollView,
  StatusBar,
  StyleSheet,
  Text,
  useColorScheme,
  View,
  NativeModules,
  Button,
} from 'react-native';

import {Colors} from 'react-native/Libraries/NewAppScreen';

function App({window = null}) {
  const colorScheme = useColorScheme();

  useEffect(() => {
    console.log('colorScheme', colorScheme);
  }, [colorScheme]);

  return window === 'mainWindow' ? <MainPage /> : <SecondaryPage />;
}

function SecondaryPage() {
  const isDarkMode = useColorScheme() === 'dark';

  const backgroundStyle = {
    backgroundColor: isDarkMode ? 'black' : 'white',
  };

  useEffect(() => {
    console.log('SecondaryPage Mount');

    return () => {
      console.log('SecondaryPage Unmount');
    };
  }, []);

  return (
    <SafeAreaView>
      <StatusBar
        barStyle={isDarkMode ? 'light-content' : 'dark-content'}
        backgroundColor={backgroundStyle.backgroundColor}
      />
      <ScrollView contentInsetAdjustmentBehavior="automatic">
        <View style={styles.content}>
          <Text style={styles.title}>Secondary Window</Text>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

function MainPage() {
  const colorScheme = useColorScheme();

  return (
    <SafeAreaView>
      <ScrollView contentInsetAdjustmentBehavior="automatic">
        <View style={styles.content}>
          <Text style={styles.title}>App Window</Text>
          <View style={styles.section}>
            <Text style={[styles.sectionTitle]}>Multi-Window Example</Text>
            <Button
              title="Open Secondary Window"
              onPress={() => NativeModules.Native.OpenSecondaryWindow()}
            />
          </View>
          <View style={styles.section}>
            <Text style={[styles.sectionTitle]}>Color Scheme</Text>
            <Text>colorScheme: {colorScheme}</Text>
          </View>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  title: {
    fontSize: 30,
    fontWeight: '600',
  },
  content: {
    flex: 1,
    padding: 25,
  },
  section: {
    marginTop: 25,
    padding: 25,
    borderRadius: 10,
  },
  sectionContainer: {
    marginTop: 32,
    paddingHorizontal: 24,
  },
  sectionTitle: {
    fontSize: 18,
    marginBottom: 20,
    fontWeight: '600',
  },
});

export default App;
